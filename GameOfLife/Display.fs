namespace Display
open Types.Model
open System

module ConsoleOutput =
    let private makeVisualBoard (g:Game) =
        g.Board
        |> List.map(fun cell -> 
            match cell with
            | {Status = Alive; Position = (x, _)} when x = g.Width  -> "O\n"
            | {Status = Dead; Position = (x,_)} when x = g.Width -> "_\n"
            | {Status = Alive; Position = (x,_)} when x <> g.Width -> "O"
            | {Status = Dead; Position = (x,_)} when x <> g.Width -> "_")
        |> String.concat ""
        |> Console.Write

    let displayBoard g=
        Console.SetCursorPosition(0,0)
        makeVisualBoard g
        printfn "Gen %i" g.Generation
        Threading.Thread.Sleep g.Refresh