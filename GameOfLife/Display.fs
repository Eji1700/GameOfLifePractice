namespace Display
open Types.Model

module ConsoleOutput =
    
    let displayBoard (g:Game) =
        [|for h in 0..g.Height do
            for w in 0..g.Width do
                let c = Cell.getCell w h g
                let x,_ = c.Position
                if c.Status = Alive then
                    sprintf "O"
                else 
                    sprintf "-"
                if x = g.Width then 
                    sprintf "\n"
        |]
        |> String.concat ""
        |> printf "%s"