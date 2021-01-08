# RefaceCore.HttpProxy

*RefaceCore.HttpProxy* 是 **.NetCore** 平台上一款，能够通过定义的接口进行 *Http* 请求的工具库。

使用 *RefaceCore.HttpProxy* 你不再需要通过 *HttpClient* 组件编写冗长的代码来完成一次请求。

你只需要定义一个 *interface* 就可以发起请求。

效果如下：

```csharp
[ClientName("Hello")] // 定义客户端名称，可以通过配置定义它对应的 Host 和 Port
public interface IHelloApiClient
{
    [HttpVerb(Verbs.Get)] // 请求谓词 : Get
    [Route("/Hello")] // 地址为 host:port/Hello
    string Hello([QueryString("name")]string name); // name 将使用 ?name= 接在 url 后
}
```

当你向组件注入此接口时，就可以直接通上面的方法发起请求了

```csharp
public class FooService : IFooService
{
    private readonly IHelloApiClient client;

    public FooService(IHelloApiClient client)
    {
        this.client = client;
    }

    public void Bar()
    {
        string result = client.Hello("Felix"); // 这里会发起请求
    }
}
```

更多阅读
* [Quick Start](docs/QuickStart.md)