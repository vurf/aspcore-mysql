# aspcore-mysql
Template to work with the MySQL database. Based on template Web Application without authentication. Made for **ASP core**.
<br>
<br>ASP.NET 5, ASP CORE, ASP MVC 6, EntityFramework 6, MySQL Connection.
<br>
<br>**Full tutorial**
<br>
* remove "dnxcore50": {} in the frameworks of project.json section;
* add libraries: Mysql.Data & Mysql.Data.Entity;
* add System.configuration with version in project.json in dnx451 {};
* create Mysql database configuration;
* create a model based on the table entity and add Table("nameOfEntity") attribute;
* create a context models;
* add model to database;
* modify startup.cs;
* display result.



#### 1. Remove dnxcore
     "frameworks": {
        "dnx451": { }
      }

#### 2. Add libraries 
      "dependencies": {
          .....
          "MySql.Data": "7.0.2-DMR",
          "MySql.Data.Entity": "7.0.2-DMR"
      },
#### 3. Add System.configuration in project.json
     "frameworks": {
        "dnx451": {
          "frameworkAssemblies": {
            "System.Data": "4.0.0.0",
            "System.configuration": "4.0.0.0"
          }
        }
     },
     
#### 4. Create MySQL database configuration 
    public class MySQLConfiguration : DbConfiguration
    {
    	public MySQLConfiguration()
    	{
    		var dataSet = (DataSet)ConfigurationManager.GetSection("system.data");
    		dataSet.Tables[0].Rows.Clear();
    		dataSet.Tables[0].Rows.Add(
    			"MySQL Data Provider",
    			".Net Framework Data Provider for MySQL",
    			"MySql.Data.MySqlClient",
    			typeof(MySqlClientFactory).AssemblyQualifiedName
    		);
    
    		SetProviderServices("MySql.Data.MySqlClient", new MySqlProviderServices());
    		SetDefaultConnectionFactory(new MySqlConnectionFactory());
    	}
    }

#### 5. Create entity with attribute
      [Table("postentity")]
      public class PostEntity
      {
        	public int Id { get; set; }
        	public string Message { get; set; }
        	public string Content { get; set; }
      }

#### 6. Create context database 
     [DbConfigurationType(typeof(MySQLConfiguration))]
     public class DataContext : DbContext 
     {
    	public DataContext(IConfiguration config) : base("Server=localhost; Database=test; Uid=root; Pwd=myPassword;")
    	{
    		/*base(config.Get("Data:DefaultConnection:ConnectionString"))*/
    	}
    
    	public virtual DbSet<PostEntity> Posts { get; set; }
     }

#### 7. Create database and insert few entities 
     CREATE TABLE `test`.`postentity` (
      `Id` INT NOT NULL AUTO_INCREMENT,
      `Message` VARCHAR(45) NULL,
      `Content` VARCHAR(45) NULL,
      PRIMARY KEY (`Id`),
      UNIQUE INDEX `Id_UNIQUE` (`Id` ASC));
      
    INSERT INTO `test`.`postentity` (`Message`, `Content`) VALUES ('hello', 'settings are correct');
    INSERT INTO `test`.`postentity` (`Message`, `Content`) VALUES ('second message', 'full description');

#### 8. Modify Startup.cs
      public void ConfigureServices(IServiceCollection services) {
        ...
        services.AddSingleton<IConfiguration>(_ => Configuration);
        services.AddScoped<MyContext>();
        ...
      }

#### 9. Display result (HomeController.cs)
      readonly DataContext _context;
      public HomeController(DataContext context)
      {
        	_context = context;
      }
        
      public IActionResult Index()
      {
        	return View(_context.Posts);
      }

in html (Views/Home/Index.cshtml)

    @model IEnumerable<WebApplication3.Models.PostEntity>
    < _u_l>
    	@foreach (var post in Model)
    	{
    		<_l_i>
    			@post.Message | @post.Content
    		</_l_i>
    	}
    </u_l>
