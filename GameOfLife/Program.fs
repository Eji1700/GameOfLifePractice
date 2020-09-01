open GameLogic
open System

[<EntryPoint>]
let main argv =
    Console.CursorVisible <- false
    Main.gameLoop (Main.createState 15 15 60 true)
    0