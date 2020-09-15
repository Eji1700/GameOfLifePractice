namespace GameLogic
open Types.Model
open System

module private Helpers =
    let rec getUserInt() =
        let i = Int32.TryParse(Console.ReadLine())
        match i with
        | true, v when v > 0 -> v
        | true, _ | false, _ ->
            printfn "Invalid entry. Enter positive whole number"
            getUserInt()

    let rec yesNo() =
        let r = Console.ReadKey().Key
        match r with 
        | ConsoleKey.Y -> true
        | ConsoleKey.N -> false
        | _ -> 
            printfn "Invalid input. y/n only."
            yesNo()

    let changeState newState g =
        {g with State = newState}

module Options =
    let private changeWidth g =
        printfn "Enter width"
        let w = Helpers.getUserInt()
        {g with Width = w}

    let private changeHeight g =
        printfn "Enter height"
        let h = Helpers.getUserInt()
        {g with Height = h}

    let private changeRefresh g =
        printfn "Enter refresh"
        let r = Helpers.getUserInt()
        {g with Refresh = r}

    let private changeSeed g =
        printfn "Random seed?"
        let r = Helpers.yesNo()
        {g with RandomSeed = r}

    let private mainMenu g =
        Helpers.changeState StartMenu g 

    let menuList =
        ['1', "Change width", changeWidth
         '2', "Change height", changeHeight
         '3', "Adjust refresh rate", changeRefresh
         '4', "Random seed?", changeSeed
         '5', "Main Menu", mainMenu]

module Start =
    let IntialGame() =
        { Width= 10
          Height= 10
          Board= []
          Generation= 0
          Refresh= 60
          RandomSeed= true
          State= StartMenu }

    let private createNewGame g =
        {g with
             Board = Board.createInitial g.Width g.Height g.RandomSeed}
    
    let private newGame g =
        createNewGame g

    let private quit g =
        Helpers.changeState Quit g

    let private options g =
        Helpers.changeState Options g

    let menuList =
        ['1', "New Game", newGame
         '2', "Options", options
         '3', "Quit", quit]

module Paused =
    let private resume g =
        Helpers.changeState Running g

    let private start g =
        Helpers.changeState StartMenu g

    let private quit g =
        Helpers.changeState Quit g