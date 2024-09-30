namespace Todo
open Todo.Enumertes
open System.ComponentModel.DataAnnotations
open System

module Models = 
    [<CLIMutable>]
    type Task = {
        Id : int 
        [<Required>]
        Subject : string 
        [<Required>]
        Description : string 
        [<Required>]
        TaskDate : DateTime
        [<Required>]
        Status : TaskStatus
    }

