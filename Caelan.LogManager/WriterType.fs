namespace Caelan.LogManager

type WriterType = 
    | File
    override t.ToString() = 
        match t with
        | File -> "File"
