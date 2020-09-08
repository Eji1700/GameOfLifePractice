open GameLogic
open System

[<EntryPoint>]
let main argv =
    Console.CursorVisible <- false
    //Main.gameLoop (Main.createState 10 10  60 true)
    DynamicConsole.test()
    Console.ReadLine() |> ignore
    0