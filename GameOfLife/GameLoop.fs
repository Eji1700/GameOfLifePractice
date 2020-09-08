namespace GameLogic
open Types.Model
open Visuals.Console

module Main =
    open System
    let createState w h r randomSeed=
        {Width = w
         Height = h
         Board = Board.createInitial w h randomSeed
         Generation = 1
         Refresh = r
         State = StartMenu}

    let pauseGame() =
        //display pause menu and parse input?
        printfn "Paused, press enter to resume"
        Console.ReadLine() |> ignore
        Console.Clear()

    let endGame newBoard g =
        let final = 
            {g with 
                Board = newBoard
                Generation = g.Generation + 1
                State = GameOver}
        Display.board final
        printfn "Game Over. Gen %i" g.Generation
        printfn "Press Enter to quit" // TODO: rework into larger logic loop
        Console.ReadLine() |> ignore

    let rec gameLoop g=
        Display.board g
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

    let rec testgameLoop g =
        match g.State with
        | StartMenu -> testgameLoop g //Display start menu
        | ToggleCells -> testgameLoop g // Display cell change ui.
        | Paused -> testgameLoop g // Display pause menu
        | Running -> testgameLoop g // Do logic checks for key entry/gameover
        | GameOver -> testgameLoop g // Show GameOver data/menu.
        | Quit -> testgameLoop g // exit game