using EbecShop.DataAccess.Repositiories;
using EbecShop.DataAccess.Repositiories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace EbecShop.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private bool _disposed;

        private Lazy<IOrderRepository> _orderRepository;
        private Lazy<IParticipantRepository> _participantRepository;
        private Lazy<IProductRepository> _productRepository;
        private Lazy<IProductTypeRepository> _productTypeRepository;
        private Lazy<ITeamRepository> _teamRepository;
        private Lazy<IOrdersProductsRepository> _ordersProductsRepository;

        public IOrderRepository Orders => _orderRepository.Value;
        public IParticipantRepository Participants => _participantRepository.Value;
        public IProductRepository Products => _productRepository.Value;
        public IProductTypeRepository ProductTypes => _productTypeRepository.Value;
        public ITeamRepository Teams => _teamRepository.Value;
        public IOrdersProductsRepository OrdersProducts => _ordersProductsRepository.Value;


        public UnitOfWork()
            : this(ConfigurationReader.ConnectionString)
        {
        }

        public UnitOfWork(string connectionString)
            : this(new SqlConnection(connectionString))
        {
        }

        public UnitOfWork(IDbConnection connection)
        {
            _connection = connection;
            _connection.Open();
            _transaction = _connection.BeginTransaction();
            resetRepositories();
        }
        

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                resetRepositories();
            }
        }

        private void resetRepositories()
        {
            _orderRepository = new Lazy<IOrderRepository>(() => new OrderRepository(_transaction));
            _participantRepository = new Lazy<IParticipantRepository>(() => new ParticipantRepository(_transaction));
            _productRepository = new Lazy<IProductRepository>(() => new ProductRepository(_transaction));
            _productTypeRepository = new Lazy<IProductTypeRepository>(() => new ProductTypeRepository(_transaction));
            _teamRepository = new Lazy<ITeamRepository>(() => new TeamRepository(_transaction));
            _ordersProductsRepository = new Lazy<IOrdersProductsRepository>(() => new OrdersProductsRepository(_transaction));
        }


        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if(_disposed == false)
            {
                if(disposing)
                {
                    if(_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if(_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            dispose(false);
        }
    }
}
