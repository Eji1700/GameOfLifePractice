open GameLogic

[<EntryPoint>]
let main argv =
    Main.gameLoop (Main.createState 10 10 60)
    0