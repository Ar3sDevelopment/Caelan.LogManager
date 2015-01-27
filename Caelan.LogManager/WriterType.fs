namespace Caelan.LogManager

type WriterType = 
    | File
    | Json
    override t.ToString() = 
        match t with
        | File -> "File"
        | Json -> "Json"
