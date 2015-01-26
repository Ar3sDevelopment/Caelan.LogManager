namespace Caelan.LogManager

open System
open System.Configuration

type FileWriterElement() =
    inherit ConfigurationElement()

    member val Path = "" with get, set

type LogConfiguration() = 
    inherit ConfigurationSection()

    [<ConfigurationProperty("fileWriter", IsKey = true)>]
    member val Test : FileWriterElement option = None with get, set