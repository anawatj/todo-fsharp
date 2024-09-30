namespace Todo
open Todo.Models
open Microsoft.EntityFrameworkCore
open System.Linq
module DataContext=
    
       type ToDoContext(options : DbContextOptions<ToDoContext>) = 
        inherit DbContext(options)
    
        [<DefaultValue>]
        val mutable Tasks : DbSet<Task>


        member public this._Tasks      with    get()   = this.Tasks 
                                              and     set value  = this.Tasks <- value 

  
        member this.TaskExist (id:int) = this.Tasks.Any(fun x -> x.Id = id)

        member this.GetTask (id:int) = this.Tasks.Find(id)

        override __.OnModelCreating(modelBuilder : ModelBuilder) = 

            modelBuilder.Entity<Task>().ToTable("Tasks") |> ignore
            modelBuilder.Entity<Task>().HasKey("Id") |> ignore
           // modelBuilder.Entity<Task>().Property(fun t->t.Id)
           //                            .HasColumnName("Id") |> ignore
            modelBuilder.Entity<Task>().Property(fun t->t.Subject)
                                       .HasColumnName("Subject")
                                       .HasMaxLength(100) |> ignore

            modelBuilder.Entity<Task>().Property(fun t->t.Description)
                                       .HasColumnName("Description") |> ignore

            modelBuilder.Entity<Task>().Property(fun t->t.TaskDate)
                                       .HasColumnName("TaskDate") |> ignore

            modelBuilder.Entity<Task>().Property(fun t->t.Status) 
                                       .HasColumnName("TaskStatus")
                                       .HasMaxLength(100)
                                       .HasConversion<string>() |> ignore
           


        

           
        

           
                              
   

