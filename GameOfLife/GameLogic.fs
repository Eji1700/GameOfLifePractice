﻿namespace GameLogic
open Types.Model

module Main =
    open System
    let createState w h r randomSeed=
        {Width = w
         Height = h
         Board = Board.createInitial w h randomSeed
         Generation = 1
         Refresh = r
         State = Menu}

    let pauseGame() =
        printfn "Paused, press enter to resume"
        Console.ReadLine() |> ignore
        Console.Clear()

    let endGame newBoard g =
        let final = 
            {g with 
                Board = newBoard
                Generation = g.Generation + 1}
        Display.ConsoleOutput.displayBoard final
        printfn "Game Over. Gen %i" g.Generation
        printfn "Press Enter to quit"
        Console.ReadLine() |> ignore

    let rec gameLoop g=
        Display.ConsoleOutput.displayBoard g
        let newBoard =
            g.Board
            |> List.map(Cell.checkSurvival g)

        let living = Board.aliveCells newBoard
        
        if living > 0 && not (Console.KeyAvailable) then
            gameLoop {g with 
                        Board = newBoard
                        Generation = g.Generation + 1}
        else
            if living = 0 then 
                endGame newBoard g
            else             
                match Console.ReadKey(true).Key with
                | ConsoleKey.Escape ->
                    endGame newBoard g

                | ConsoleKey.Spacebar ->
                    pauseGame()
                    gameLoop {g with
                                Board = newBoard
                                Generation = g.Generation + 1}

                | _ -> 
                   gameLoop {g with
                                Board = newBoard
                                Generation = g.Generation + 1}