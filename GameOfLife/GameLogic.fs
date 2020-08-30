namespace GameLogic
open Types.Model

module Main =
    open System
    let createState w h =
        {Width = w
         Height = h
         Board = Board.createInitial w h
         Generation = 1}

    let rec gameLoop g=
        printfn "Gen %i" g.Generation
        let newBoard =
            g.Board
            |> Array.map(Cell.checkSurvival g)
        if Board.aliveCells newBoard > 0 
            && not (Console.KeyAvailable 
                    && Console.ReadKey(true).Key = ConsoleKey.Escape) then
            gameLoop {g with 
                        Board = newBoard
                        Generation = g.Generation + 1}
        else
            printfn "Game Over. Gen %i" g.Generation
            Console.ReadLine() |> ignore