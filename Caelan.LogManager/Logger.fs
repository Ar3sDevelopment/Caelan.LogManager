namespace Caelan.LogManager

type Logger internal (name, writers : seq<ILogWriter>) = 
    
    member __.Log logType message = 
        for writer in writers do
            (logType, message) ||> writer.Log
    
    member __.LogException logType message ex = 
        for writer in writers do
            (logType, message, ex) |||> writer.LogException

type Logger<'T> internal (writers) = 
    inherit Logger(typeof<'T>.GetType().Name, writers)
