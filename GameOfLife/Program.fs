open GameLogic

[<EntryPoint>]
let main argv =
    Main.gameLoop (Main.createState 15 15 60 true)
    0