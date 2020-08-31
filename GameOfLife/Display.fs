namespace Display
open Types.Model
open System

module ConsoleOutput =
    
    let private makeVisualBoard (g:Game) =
        [|for h in 0..g.Height do
            for w in 0..g.Width do
                let c = Cell.getCell w h g
                let x,_ = c.Position

                if c.Status = Alive then "O"
                else "_"

                if x = g.Width then "\n" |]
        |> String.concat ""
        |> Console.Write

    let displayBoard g=
        Console.SetCursorPosition(0,0)
        makeVisualBoard g
        printfn "Gen %i" g.Generation
        Threading.Thread.Sleep g.Refresh