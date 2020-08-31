open GameLogic

[<EntryPoint>]
let main argv =
    System.Console.CursorVisible <- false
    Main.gameLoop (Main.createState 10 10 60)
    0