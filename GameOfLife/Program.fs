﻿open GameLogic
open System

[<EntryPoint>]
let main argv =
    Console.CursorVisible <- false
    Main.gameLoop (Main.createState 50 50 60 true)
    0