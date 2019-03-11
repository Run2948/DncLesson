# ��һ�� ��������
* ��������
  * Asp.Net Core Mvc��.NET Coreƽ̨�µ�һ��WebӦ�ÿ������
    * ����WebӦ���ص�
    * .NET Core��ƽ̨�������
    * MVC���ģʽ��һ��ʵ��
* ����׼��
  * ��װ���°�[Visual Studio 2017](https://visualstudio.microsoft.com/zh-hans/)
  * ��װ���°�[.NET Core Sdk](https://dotnet.microsoft.com/download)

# �ڶ��� �������Ľ���
* ���������巽ʽ��
    * ������`Controller`��β
    * ʹ��`ControllerAttribute`��ע
``` csharp
    public class TestController : Controller
    {

    }

    [Controller]
    public class Test : Controller
    {

    }
```
* Ĭ��·�ɹ��� 
  * ����/��������/����
  * `{Domain}/{Controller}/{Action}`

* ������ʽ
  * `QueryString`:  ?name=zhangsan&age=22
  * `Form` 
  * `Cookie`
  * `Session`
  * `Header`

* HttpRequest 
  * HttpRequest ���û��������
  * �ṩ��ȡ�������ݵ�����
    * Cookies,Headers,Query,QueryString,Form
```csharp
        public IActionResult Hello()
        {
            // Query
            var name = Request.Query["name"];
            // QueryString
            var query = Request.QueryString.Value;
            // Form
            var username = Request.Form["username"];
            // Cookies
            var cookie = Request.Cookies["item"];
            // Headers
            var headers = Request.Headers["salt"];

            return Content("Hello");
        }
```

* HttpContext
  * HttpContext���û�����������
  * �ṩSession���Ի�ȡSession����
    * Session.Set ����
    * Session.Remove �Ƴ�
    * Session.TryGetValue ��ȡ����
```csharp
        public IActionResult Hello()
        {       
            // byte[]
            HttpContext.Session.Set("byte", new byte[] { 1, 2, 3, 4, 5 });
            var bytes = HttpContext.Session.Get("byte");
            // string
            HttpContext.Session.SetString("name", "tom");
            var name = HttpContext.Session.GetString("name");
            // int
            HttpContext.Session.SetInt32("id", 20);
            var id = HttpContext.Session.GetInt32("id");
            HttpContext.Session.Remove("name");
            HttpContext.Session.Clear();
            
            return Content("Hello");
        }
```

* ���ݰ�
  * ���û���������ݰ󶨵������������Ĳ�����
  * ֧�ּ��������Զ�������
  * �󶨹��������������������������һ��
    * ���ѯ�ַ���key���Ƹ�����һ��
    * Form�����Ƹ�����һ��
```csharp
        public IActionResult Hello(RequestModel request,int? age)
        {
            // ��ѯ�ַ���
            var test = Request.Query["test"];
            // ������
            var userAge = age;
            // �Զ�������
            var name = request.Name;

            return Content("Hello");
        }

        public class RequestModel
        {
            public string Name { get; set; }       
        }
```

* ���ݲ���
  * �����Controller��β�Ķ��ǿ����������������������һЩҵ��������ʱ��Ҳ����Controller��β����ô�죿
  * `NonControllerAttribute`
```csharp
    /// <summary>
    /// ����ʦ������
    /// </summary>
    [NonController]
    public class AuctionController
    {

    }   
```

* ��������

| ����         | ����Դ              |    
| ---------- | --------------------- |      
| FromHeaderAttribute | ����ͷ���� |    
| FromRouteAttribute| ·������ |  
| FromBodyAttribute| ������ |  
| FromFormAttribute| ������ |  
| FromQueryAttribute| ��ѯ�ַ��� |  
| FromServicesAttribute| ����ע�� |  
```csharp
        public IActionResult Say(
            [FromForm]string name,
            [FromQuery]int age,
            [FromHeader] string salt,
            [FromBody] string content
            )
        {
            return View();
        }
```

* ���Բ���
  * ͨ���������β�����Ӱ����߼�
  * �����չ

* IActionResult 
  * ��������ӿ�
  * ����ʵ�� 
    * JsonResult������JSON�ṹ���� 
    * RedirectResult����ת���µ�ַ
    * FileResult�������ļ�
    * ViewResult��������ͼ����
    * ContentResult���ı�����

# ������ ��ͼ���

* ���ݴ���
  * ViewData
  * ViewBag
  * tempData
  * Model
  * Session
  * Cache

| ViewData | ViewBag |
| -------- | ------- |
| ��ֵ�� | ��̬���� |
| ������ | ViewData�ķ�װ |
| ֧���������� | ��̬���� |

| TempData | Cache | Session |
| -------- | ------- | ------ |
| ��ͼ���� | Ӧ�ó��򼶱� | �Ự���� |
| ֻ��������һ�� | �������˱��� | �������˱��� |
| �ɶ�θ�ֵ | ��������Ч�� | ��ֵ����ʽ |
| ��ֵ����ʽ | ��ֵ����ʽ |  |

* Cache
  * ��.NET Frameworkʱ����ͬ��һ��ȫ��ʵ��
  * IMemoryCache�ӿ�
  * ����ע�뷽ʽ��ȡ
  * IMemoryCache.Get/Set��������

```csharp
    [Controller]
    public class Test : Controller
    {
        private readonly IMemoryCache _cache;

        public Test(IMemoryCache memoryCache)
        {
            this._cache = memoryCache;   
        }

        public IActionResult ReadCache()
        {
            _cache.Set("name","tom");
            _cache.Get("name");

            _cache.Set("age",30);
            _cache.Get("age");

            User tom = new User(){ Name = "admin",Pwd = "123456"};
            _cache.Set<User>("user",tom);
            _cache.Get<User>("user");
            return Content("ok");
        }
    }

    public class User
    {
        public string Name { get; set; }
        public string Pwd { get; set; }
    }
```

* ViewStart
  * ��_ViewStart.cshtml�������̶����ƣ����ܸ���
  * һ�������ͼ����Ŀ¼�ĸ�Ŀ¼��
  * �Զ�ִ�У������ֹ�����
  * ��Ҫ��ViewStart����������ҵ�����
* ViewImport
  * ��_ViewImport.cshtml�������̶����ƣ����ܸ���
  * ֻ���������
  * һ�������ͼ����Ŀ¼�ĸ�Ŀ¼��
  * �Զ�ִ�У������ֹ�����
  * ��ͼ�п���ʹ��@using�ؼ����������������ռ�
  * ͨ��ViewImport��ȫ���Ե������ռ����룬������ÿ��ҳ��������Ĺ�����

# ���Ŀ� ������֤

* ������֤����`ValidationAttribute`
```csharp
  public abstract class ValidationAttribute : Attribute
  {
    /// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationAttribute"></see> class.</summary>
    protected ValidationAttribute();

    /// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationAttribute"></see> class by using the function that enables access to validation resources.</summary>
    /// <param name="errorMessageAccessor">The function that enables access to validation resources.</param>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="errorMessageAccessor">errorMessageAccessor</paramref> is null.</exception>
    protected ValidationAttribute(Func<string> errorMessageAccessor);

    /// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationAttribute"></see> class by using the error message to associate with a validation control.</summary>
    /// <param name="errorMessage">The error message to associate with a validation control.</param>
    protected ValidationAttribute(string errorMessage);

    /// <summary>Gets or sets an error message to associate with a validation control if validation fails.</summary>
    /// <returns>The error message that is associated with the validation control.</returns>
    public string ErrorMessage { get; set; }

    /// <summary>Gets or sets the error message resource name to use in order to look up the <see cref="P:System.ComponentModel.DataAnnotations.ValidationAttribute.ErrorMessageResourceType"></see> property value if validation fails.</summary>
    /// <returns>The error message resource that is associated with a validation control.</returns>
    public string ErrorMessageResourceName { get; set; }

    /// <summary>Gets or sets the resource type to use for error-message lookup if validation fails.</summary>
    /// <returns>The type of error message that is associated with a validation control.</returns>
    public Type ErrorMessageResourceType { get; set; }

    /// <summary>Gets the localized validation error message.</summary>
    /// <returns>The localized validation error message.</returns>
    protected string ErrorMessageString { get; }

    /// <summary>Gets a value that indicates whether the attribute requires validation context.</summary>
    /// <returns>true if the attribute requires validation context; otherwise, false.</returns>
    public virtual bool RequiresValidationContext { get; }

    /// <summary>Applies formatting to an error message, based on the data field where the error occurred.</summary>
    /// <param name="name">The name to include in the formatted message.</param>
    /// <returns>An instance of the formatted error message.</returns>
    public virtual string FormatErrorMessage(string name);

    /// <summary>Checks whether the specified value is valid with respect to the current validation attribute.</summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="validationContext">The context information about the validation operation.</param>
    /// <returns>An instance of the <see cref="System.ComponentModel.DataAnnotations.ValidationResult"></see> class.</returns>
    public ValidationResult GetValidationResult(
      object value,
      ValidationContext validationContext);

    /// <summary>Determines whether the specified value of the object is valid.</summary>
    /// <param name="value">The value of the object to validate.</param>
    /// <returns>true if the specified value is valid; otherwise, false.</returns>
    public virtual bool IsValid(object value);

    /// <summary>Validates the specified value with respect to the current validation attribute.</summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="validationContext">The context information about the validation operation.</param>
    /// <returns>An instance of the <see cref="System.ComponentModel.DataAnnotations.ValidationResult"></see> class.</returns>
    protected virtual ValidationResult IsValid(
      object value,
      ValidationContext validationContext);

    /// <summary>Validates the specified object.</summary>
    /// <param name="value">The object to validate.</param>
    /// <param name="validationContext">The <see cref="T:System.ComponentModel.DataAnnotations.ValidationContext"></see> object that describes the context where the validation checks are performed. This parameter cannot be null.</param>
    /// <exception cref="T:System.ComponentModel.DataAnnotations.ValidationException">Validation failed.</exception>
    public void Validate(object value, ValidationContext validationContext);

    /// <summary>Validates the specified object.</summary>
    /// <param name="value">The value of the object to validate.</param>
    /// <param name="name">The name to include in the error message.</param>
    /// <exception cref="T:System.ComponentModel.DataAnnotations.ValidationException"><paramref name="value">value</paramref> is not valid.</exception>
    public void Validate(object value, string name);
  }
```

* ����������֤
  * RequiredAttribute
  * RegularExpressionAttribute
  * CompareAttribute
  * RangeAttribute
  * MaxAttribute
  * MinAttribute
  * StringLengthAttribute
  * DataTypeAttribute

* ��������ʹ��
  * ʹ�ð�����֤��������������
  * ʹ��ModelState.IsValid�ж��Ƿ����Ҫ��

* ǰ��ʹ��
  * ����ǿ������ͼ�����ݰ�����֤�����ҵ������ģ��
  * ʹ��HtmlHelper.ValidationFor��ʼǰ����֤����
  * ʹ��HtmlHelper.ValidationMessageFor������ʾ����

```csharp

    public class UserLogin
    {
        [Required(ErrorMessage = "�û�������Ϊ��")]
        [StringLength(10,ErrorMessage = "�û������Ȳ��ܳ���10λ")]
        public string UserName { get; set; }
          
        //[Required(ErrorMessage = "���벻��Ϊ��")]
        [StringLength(6,ErrorMessage = "���볤�Ȳ��ܳ���6λ")]
        public string Password { get; set; }
    }

```
```csharp
    public class FormController : Controller
    {
        public IActionResult Index()
        {
            return View(new UserLogin());
        }

        public IActionResult PostData(UserLogin login)
        {
            return Content(ModelState.IsValid?"������Ч":"������Ч");
        }
    }

```
```csharp
@model Lesson2.Models.UserLogin
@{
    Layout = null;
}

<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>Index</title>
    <script src="~/lib/jquery/dist/jquery.min.js"></script>
    <script src="~/lib/jquery-validation/dist/jquery.validate.min.js"></script>
    <script src="~/lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js"></script>
</head>
<body>
    <form asp-action="PostData" method="post">
        <table>
            <tr>
                <td>�û���</td>
                <td>@Html.TextBoxFor(m => m.UserName)</td>
                <td>@Html.ValidationMessageFor(m => m.UserName)</td>
            </tr>
            <tr>
                <td>����</td>
                <td>@Html.PasswordFor(m => m.Password)</td>
                <td>@Html.ValidationMessageFor(m => m.Password)</td>
            </tr>
            <tr>
                <td></td>
                <td><input type="submit" value="��¼" /></td>
                <td></td>
            </tr>
        </table>
    </form>
</body>
</html>

```

# ����� ·�ɹ���
* ·��
  * �����û����������������֮ǰ��ӳ���ϵ

* ·������
  * IRouteBuilder
    * ͨ��MapRoute��������·��ģ��

```csharp
    app.UseMvc(routes =>
    {
        routes.MapRoute(
            name: "default",
            template: "{controller=Home}/{action=Index}/{id?}");

        routes.MapRoute(
            name: "admin_default",
            template: "admin/{controller=Home}/{action=Index}/{id?}");
    });
```

  * RouteAttribute
    * Ӧ���ڿ�������������
    * ͨ��Template��������·��ģ��

```csharp
    [Route("admin/form")]
    public class FormController : Controller
    {
        [Route("index")]
        public IActionResult Index()
        {
            return View(new UserLogin());
        }

        public IActionResult PostData(UserLogin login)
        {
            return Content(ModelState.IsValid?"������Ч":"������Ч");
        }
    }
```

* ·��Լ��
  * ��·�����ݽ���Լ��
  * ֻ��Լ��������������ƥ��ɹ�

| Լ�� | ʾ�� | ˵�� |
| ---- | ---- | ---- |
| required | "Product/{ProductName:required}" | ������ѡ |
| alpha | "Product/{ProductName:alpha}" | ƥ����ĸ����Сд���� |
| int | "Product/{ProductId:int}" | ƥ��int���� |
| ������ | ������ | ������ |
| composite | "Product/{ProductId:composite}" | ƥ��composite���� |
| length | "Product/{ProductName:length(5)}" | ���ȱ�����5���ַ� |
| length | "Product/{ProductName:length(5)}" | ������5-10֮�� |
| maxlength | "Product/{ProductId:maxlength(10)}" | ��󳤶�Ϊ10 |
| minlength | "Product/{ProductId:minlength(3)}" | ��С����Ϊ3 |
| min | "Product/{ProductId:min(3)}" | ���ڵ���3 |
| max | "Product/{ProductId:max(10)}" | С�ڵ���10 |
| range | "Product/{ProductId:range(5,10)}" | ��Ӧ��������5-10֮�� |
| regex | "Product/{ProductId:regex(^\d{4}$)}" | ����ָ����������ʽ |

* ·������
  * ·������Ҳ���������ݵ�һ����
  * ·�������������һ����Ҳ���԰󶨵�������
  * Ĭ����ͨ�����ƽ���ƥ�䣬Ҳ����ͨ��`FormRouteAttribute`ƥ�������·�����ݵ�ӳ���ϵ

```csharp
    public IActionResult Index([FromRoute] int? id)
    {
        return View();
    }
```

# ������ Ӧ�÷����벿��
* ����
  * ��������
    * ʹ��`Visual Studio`����Ӧ�ã�`��Ŀ�Ҽ� -> ���� -> ������ʽѡ��...`
    * ʹ��`dotnet publish`�����й��߷�����`dotnet publish --configuration Release --runtime win7-x64 --output c:\svc`
* ��ͼԤ����
  * ��������ʱ������̣������ٶȿ�
  * Ԥ����������������С
  * ����ͨ��MvcRazorCompileOnPublish�����Ƿ�����Ĭ���ǿ���״̬
    * �ر���ͼԤ���룺
      * ����Ŀ��`.csproj`�ļ�
      * ����`MvcRazorCompileOnPublish`Ϊ`false`
```csharp
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>netcoreapp2.1</TargetFramework>
    <!-- �ر���ͼԤ���� -->
    <MvcRazorCompileOnPublish>false</MvcRazorCompileOnPublish>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.App" />
    <PackageReference Include="Microsoft.AspNetCore.Razor.Design" Version="2.1.2" PrivateAssets="All" />
    <PackageReference Include="Microsoft.VisualStudio.Web.CodeGeneration.Design" Version="2.1.1" />
  </ItemGroup>

</Project>
```
* ����
  * IIS ����
    * Ŀ�������װ��Ӧ�汾��[.NET Core Sdk](https://dotnet.microsoft.com/learn/dotnet/hello-world-tutorial/install)
    * ��װ[.NET Core Windows Server �йܳ���](https://www.microsoft.com/net/permalink/dotnetcore-current-windows-runtime-bundle-installer)
    * Ӧ�ó���صġ�.NET CLR�汾������Ϊ�����йܴ��롱
    * ![](https://docs.microsoft.com/zh-cn/aspnet/core/host-and-deploy/iis/index/_static/edit-apppool-ws2016.png?view=aspnetcore-2.2)
  * ����������
    * ������һ��exeֱ������
    * ��������IIS
    * RuntimeIdentifier
    * [.NET Core RID Catalog](https://docs.microsoft.com/en-us/dotnet/core/rid-catalog)
```xml
<!-- ������ܵĲ��� (FDD) -->
<PropertyGroup>
  <TargetFramework>netcoreapp2.2</TargetFramework>
  <RuntimeIdentifier>win7-x64</RuntimeIdentifier>
  <SelfContained>false</SelfContained>
  <IsTransformWebConfigDisabled>true</IsTransformWebConfigDisabled>
</PropertyGroup>
<!-- �������� (SCD) -->
<PropertyGroup>
  <TargetFramework>netcoreapp2.2</TargetFramework>
  <RuntimeIdentifier>win7-x64</RuntimeIdentifier>
  <IsTransformWebConfigDisabled>true</IsTransformWebConfigDisabled>
</PropertyGroup>
    ...
    ...
    ...
```