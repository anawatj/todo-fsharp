namespace Todo.Controllers

open Microsoft.AspNetCore.Mvc
open Todo.DataContext
open System.Collections.Generic
open Todo.Models
open Microsoft.EntityFrameworkCore
open System.Linq;
open System

[<ApiController>]
[<Route("api/[controller]")>]
type TasksController private () = 
    inherit ControllerBase ()
    new (context : ToDoContext) as this =
        TasksController () then
        this._Context <- context
        
    

    [<HttpGet>]
    member this.Get() :IActionResult =
        let tasks = this._Context.Tasks.ToList<Task>()
        if(tasks.Count=0) then 
            base.NotFound("NOT FOUND!")
        else
            base.Ok(tasks)

    [<HttpGet("{id}")>]
    member this.Get(id:int) : IActionResult=
        if base.ModelState.IsValid then 
          if not ( this._Context.TaskExist(id) ) then 
               base.NotFound("NOT FOUND! " + id.ToString())
            else
                base.Ok(this._Context.GetTask(id))
        else
            base.BadRequest(base.ModelState)


    [<HttpPost>]
    member this.Post([<FromBody>] _Task : Task) :IActionResult=
        if (base.ModelState.IsValid) then 
            if not( isNull _Task.Subject ) then
                if ( _Task.Id <> 0 ) then 
                    base.BadRequest("BAD REQUEST, the Task Id is autoincremented")
                else 
                        this._Context.Tasks.Add(_Task) |> ignore
                        this._Context.SaveChanges() |> ignore
                        base.Ok(this._Context._Tasks)
            else
               base.BadRequest("BAD REQUEST!, the field Initials can not be null")                  
        else
            base.BadRequest(base.ModelState)


    [<HttpPut("{id}")>]
    member this.Put( id:int, [<FromBody>] _Task : Task) :IActionResult=
        if (base.ModelState.IsValid) then 
            if not( isNull _Task.Subject ) then
                if (_Task.Id <> id) then 
                    base.BadRequest()
                else
                        try
                            this._Context.Tasks.Update(_Task) |> ignore
                            this._Context.SaveChanges() |> ignore
                            base.Ok(_Task)
                        with ex ->
                            if not( this._Context.TaskExist(id)) then
                                base.NotFound()
                            else 
                                base.BadRequest()
            else
                base.BadRequest()                             
        else    
            base.BadRequest(base.ModelState)

    [<HttpDelete("{id}")>]
    member this.Delete(id:int) :IActionResult =
        if (base.ModelState.IsValid) then 
            if not( this._Context.TaskExist(id) ) then 
                base.NotFound()
            else (
                    this._Context.Tasks.Remove(this._Context.GetTask(id)) |> ignore
                    this._Context.SaveChanges() |> ignore
                    base.Ok("Delete Success")
            )
        else
            base.BadRequest(base.ModelState)


    [<DefaultValue>]
    val mutable _Context : ToDoContext


