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

        let living = Board.aliveCells newBoard
        if living > 0 
            && not (Console.KeyAvailable 
                    ) then
            gameLoop {g with 
                        Board = newBoard
                        Generation = g.Generation + 1}
        else
            if living = 0 then 
                let final = {g with 
                                Board = newBoard
                                Generation = g.Generation + 1}
                Display.ConsoleOutput.displayBoard final
                printfn "Game Over. Gen %i" g.Generation
            
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
                    printfn "Paused, press enter to resume"
                    Console.ReadLine() |> ignore
                    Console.Clear()
                    gameLoop {g with
                                Board = newBoard
                                Generation = g.Generation + 1}

                | _ -> 
                   gameLoop {g with
                                Board = newBoard
                                Generation = g.Generation + 1}