# Open Api Genen



## Install CLI Tools

* [Install Docs](https://openapi-generator.tech/docs/installation)
* npm

```
npm install @openapitools/openapi-generator-cli -g
```

* JAR

```
# for Windows
Invoke-WebRequest -OutFile openapi-generator-cli.jar https://repo1.maven.org/maven2/org/openapitools/openapi-generator-cli/7.2.0/openapi-generator-cli-7.2.0.jar

# for Mac / Linux
wget https://repo1.maven.org/maven2/org/openapitools/openapi-generator-cli/7.2.0/openapi-generator-cli-7.2.0.jar -O openapi-generator-cli.jar
```



---

## Install Pagckages



```
dotnet add package RestSharp
dotnet add package Newtonsoft.Json
dotnet add package JsonSubTypes
dotnet add package System.ComponentModel.Annotations
dotnet add package Polly
```





---

## Source Generator

* **Example (JAR)**
    * [Additional Properties for C#](https://openapi-generator.tech/docs/generators/csharp)

```
java -jar openapi-generator-cli-7.2.0.jar generate -i swagger.json -g csharp -o ./
```



## Reference

* [OpenAPI Generator - Github](https://github.com/OpenAPITools/openapi-generator)