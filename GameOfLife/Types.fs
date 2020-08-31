namespace Types

module Model =
    type Status =
    | Alive
    | Dead

    type Cell = {Status: Status; Position: int * int}
    type Board = array<Cell>
    type State = 
    | Menu
    | Running
    | GameOver

    type Game = 
        {Width:int
         Height:int
         Board:Board
         Generation:int
         Refresh:int
         State:State }

    module Cell =
        let toggle cell =
            match cell.Status with
            | Alive -> {cell with Status = Dead}
            | Dead -> {cell with Status = Alive}

        let isAlive cell =
            match cell.Status with
            | Alive -> 1
            | Dead -> 0

        let private getNeighborsPosition cell=
            let x,y = cell.Position
            [|for ii in x-1..x+1 do
                 for jj in y-1..y+1 do
                     if not (ii = x && jj = y) then
                        ii, jj|]

        let getCell x y g  =
            g.Board
            |> Array.filter (fun a -> a.Position = (x,y))
            |> Array.exactlyOne

        let private liveCellRules cell i =
            match i with
            | i when i < 2 -> {cell with Status = Dead}
            | i when i > 3 -> {cell with Status = Dead}
            | _ -> cell

        let private deadCellRules cell i =
            match i with
            | i when i = 3 -> {cell with Status = Alive}
            | _ -> cell

        let cellRules cell i =
            match cell.Status with
            | Alive -> liveCellRules cell i
            | Dead -> deadCellRules cell i

        let checkSurvival g cell =
            getNeighborsPosition cell
            |> Array.filter(fun (a,b) -> a >= 0 && a <= g.Width && b >= 0 && b <= g.Height)
            |> Array.map(fun (a,b) -> getCell a b g)
            |> Array.sumBy(isAlive)
            |> fun i -> cellRules cell i

    module Board =
        let createInitial w h randomSeed : Board= 
            let rng = System.Random()
            [|for x in 0..w do
                for y in 0..h do
                    if randomSeed then
                        if rng.Next(1,3) > 1 then 
                            {Status = Dead; Position = x,y}
                        else
                            {Status = Alive; Position = x,y}
                    else
                        {Status = Dead; Position = x,y}|]

        let aliveCells (b:Board) =
            b
            |> Array.filter(fun cell -> cell.Status = Alive)
            |> Array.length