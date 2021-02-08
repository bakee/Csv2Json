# Csv2Json

This is a tool to convert csv file to json. The following format is expected from the csv file:

||||
|:---:|:----:|:---:|
|Object 1|Property 1|Value of Property 1|
||Property 2|Value of Property 2|
||Property 3|Value of Property 3|
| Object 2|||
|| Property 1|Value of Property 1|
|| Property 20|Value of Property 20|
|Object 3|Property 10|Value of Property 10|
|| Property 20|Value of Property 20|
|| Property 4|Value of Property 4|

Output:
```json
{
   "Object 1":{
      "Property 1":"Value of Property 1",
      "Property 2":"Value of Property 2",
      "Property 3":"Value of Property 3"
   },
   "Object 2":{
      "Property 1":"Value of Property 1",
      "Property 20":"Value of Property 20"
   },
   "Object 3":{
      "Property 10":"Value of Property 10",
      "Property 20":"Value of Property 20",
      "Property 4":"Value of Property 4"
   }
}

```

## How to build
Build the project using visual studio or dot-net core.

## How to use
```bash
./Csv2Json ./input.csv
```

By default the json output will be in console. If you want to put it in a file you can redirect the output to a file e.g.

```bash
./Csv2Json ./input.csv > output.json
```

