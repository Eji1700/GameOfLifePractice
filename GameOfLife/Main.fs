module Main 
open Types.Model
open Visuals.Console

let rec GameLoop g =
    //Display whatever, if needed?
    //run logic if needed
    //listen for key? not (Console.KeyAvailable?)
    //on press pass to state parser and proceed?
    //always listen for key to allow esc to quit?
    match g.State with
    | StartMenu -> 
        Display.StartMenu g //Display start menu
        |> GameLoop 
    | ToggleCells -> GameLoop g // Display cell change ui.
    | NewGame ->
        g.Board |> printfn "%A"
        Display.Board g
        |> GameLoop  // Create a new game and start it.
    | Paused -> GameLoop g // Display pause menu
    | Options -> 
        Display.OptionsMenu g // Display options
        |> GameLoop 
    | Running -> 
        Display.Board g
        |> Game.CalcGen
        |> GameLoop  // Do logic checks for key entry/gameover
    | GameOver -> GameLoop g // Show GameOver data/menu.
    | Quit -> ()// exit game