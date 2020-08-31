namespace GameLogic
open Types.Model

module Main =
    open System
    let createState w h r=
        {Width = w
         Height = h
         Board = Board.createInitial w h
         Generation = 1
         Refresh = r}

    let rec gameLoop g=
        Display.ConsoleOutput.displayBoard g
        let newBoard =
            g.Board
            |> Array.map(Cell.checkSurvival g)

        if Board.aliveCells newBoard > 0 
            && not (Console.KeyAvailable 
                    ) then
            gameLoop {g with 
                        Board = newBoard
                        Generation = g.Generation + 1}
        else
            match Console.ReadKey(true).Key with
            | ConsoleKey.Escape ->
                let final = {g with 
                                Board = newBoard
                                Generation = g.Generation + 1}
                Display.ConsoleOutput.displayBoard final
                printfn "Game Over. Gen %i" g.Generation
                Console.ReadLine() |> ignore

            | ConsoleKey.Spacebar ->
                Console.ReadLine() |> ignore
                gameLoop {g with
                            Board = newBoard
                            Generation = g.Generation + 1}

            | _ -> 
               gameLoop {g with
                            Board = newBoard
                            Generation = g.Generation + 1}