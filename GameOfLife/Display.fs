namespace Display
open Types.Model
open System

module ConsoleOutput =
    let private makeVisualBoard (g:Game) =
        let (|Eq|Ne|) x = if x = g.Width then Eq else Ne
        g.Board
        |> List.map(fun cell -> 
            match cell with
            | {Status = Alive; Position = (Eq, _)} -> "O\n"
            | {Status = Dead; Position = (Eq,_)} -> "_\n"
            | {Status = Alive} -> "O"
            | {Status = Dead} -> "_")
        |> String.concat ""
        |> Console.Write

    let displayBoard g=
        Console.SetCursorPosition(0,0)
        makeVisualBoard g
        printfn "Gen %i" g.Generation
        Threading.Thread.Sleep g.Refresh