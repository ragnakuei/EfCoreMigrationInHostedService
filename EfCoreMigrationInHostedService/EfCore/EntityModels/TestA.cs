using System;

namespace EfCoreMigrationInHostedService.EfCore.EntityModels
{
    public class TestA
    {
        public int Id { get; set; }

        public Guid Guid { get; set; }

        public int Age { get; set; }

        public string Name { get; set; }
    }
}