﻿using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using ProjeOdevi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjeOdevi
{
    public class Database
    {
        private static ISessionFactory _sessionFactory;

        public static ISession Session
        {
            get
            {
                return (ISession)HttpContext.Current.Items["Session"];
            }
        }

        public static void Configure()
        {
            var config = new Configuration();
            config.Configure();

            var mapper = new ModelMapper();
            mapper.AddMapping<KisiMap>();
            mapper.AddMapping<PostMap>();
            mapper.AddMapping<KisiTakipMap>();

            config.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());
            _sessionFactory = config.BuildSessionFactory();

        }

        public static void OpenSession()
        {
            HttpContext.Current.Items["Session"] = _sessionFactory.OpenSession();
        }

        public static void CloseSession()
        {
            var session = HttpContext.Current.Items["Session"] as ISession;

            if (session != null)
            {
                session.Close();
            }
            HttpContext.Current.Items.Remove("SessionFactory");
        }
    }
}