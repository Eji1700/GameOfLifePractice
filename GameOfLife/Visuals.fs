namespace Visuals

module Console =
    open Types.Model
    open System
    open GameLogic

    module Display =
        let private makeBoard (g:Game) =
            let (|Eq|Ne|) x = 
                if x = g.Width then Eq else Ne
            g.Board
            |> List.map(fun cell -> 
                match cell with
                | {Status = Alive; Position = (Eq, _)} -> "O\n"
                | {Status = Dead; Position = (Eq,_)} -> "_\n"
                | {Status = Alive} -> "O"
                | {Status = Dead} -> "_")
            |> String.concat ""
            |> Console.Write

        let Board g=
            Console.SetCursorPosition(0,0)
            makeBoard g
            printfn "Gen %i" g.Generation
            Threading.Thread.Sleep g.Refresh
            g

        let StartMenu g  =
            DynamicConsole.StartFlow g

        let OptionsMenu g =
            DynamicConsole.OptionFlow g