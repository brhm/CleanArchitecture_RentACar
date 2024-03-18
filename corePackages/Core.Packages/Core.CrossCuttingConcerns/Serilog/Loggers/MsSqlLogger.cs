using Core.CrossCuttingConcerns.Serilog.ConfigurationModels;
using Core.CrossCuttingConcerns.Serilog.Messages;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Serilog.Loggers;

public class MsSqlLogger:LoggerServiceBase
{
    private readonly IConfiguration _configuration;
    public MsSqlLogger(IConfiguration configuration)
    {
        _configuration = configuration;

        MsSqlConfiguration logConfig= 
            _configuration.GetSection("SerilogConfiguration:MsSqlConfiguration").Get<MsSqlConfiguration>() 
            ?? throw new Exception(SerilogMessages.NullOptionsMessage);

        MSSqlServerSinkOptions sinkOptions = new()
        {
            TableName=logConfig.TableName,
            AutoCreateSqlDatabase=logConfig.AutoCreateSqlTable
        };
        ColumnOptions columnOptions = new();
        global::Serilog.Core.Logger serilog = new LoggerConfiguration().WriteTo
            .MSSqlServer(logConfig.ConnectionString, sinkOptions, columnOptions: columnOptions)
            .CreateLogger();

        Logger=serilog;

    }
}
