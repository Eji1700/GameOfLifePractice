open GameLogic
open System

[<EntryPoint>]
let main argv =
    Console.CursorVisible <- false
    Intital.GameState()
    |> Main.GameLoop  
    0