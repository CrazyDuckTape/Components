using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NHibernate;
using System;

namespace Domain
{
    internal class DBConnetionManager
    {
        private string _connectionString;
        private DBStatus _connectionStatus;
        private IConfiguration _configuration;
        private ILogger _logger;

        private DBConnetionManager() { }

        public DBConnetionManager(IConfiguration configuration, ILogger logger)
        {
            _configuration = configuration;
            _connectionStatus = DBStatus.NotConnected;
            _logger = logger;
        }

        public ISessionFactory StartDBConnection()
        {
            if(_connectionStatus == DBStatus.Connected || _connectionStatus == DBStatus.Connecting)
            {
                throw new Exception("A database connection is already established. Please close the current connection");
            }

            SetDBConnectionString();

            if(string.IsNullOrWhiteSpace(_connectionString))
            {
                throw new Exception("There is none valid database connection string.");
            }

            var sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(_connectionString))
                .BuildSessionFactory();

            return sessionFactory;
        }

        private void SetDBConnectionString()
        {
            if (!string.IsNullOrWhiteSpace(_connectionString))
            {
                _logger.LogWarning("An database connection string already exists");
            }

            var conn = _configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrWhiteSpace(_connectionString) || !_connectionString.Equals(conn))
            {
                _connectionString = conn;
                _logger.LogInformation("A new database connection string was saved");
            }
        }
    }

    enum DBStatus
    {
        Closed,
        Connected,
        Connecting,
        NotConnected
    }
}
