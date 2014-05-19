using Xunit;
using Should;
using System;

namespace AutoMapper.UnitTests.Bug
{

    namespace AssertValidConfigurationIgnoreProvidedTypeMaps
    {
        public class Bug505 : AutoMapperSpecBase
        {
            public class Source
            {
                public int Value { get; set; }
            }
            public class Destination
            {
                public int Value { get; set; }
            }

            public class Destination2
            {
                public int Value2 { get; set; }
            }

            [Fact]
            public void Should_call_ctor_once()
            {
                Mapper.CreateMap<Source, Destination>();
                Mapper.CreateMap<Source, Destination2>();

                var typeMap = Mapper.FindTypeMapFor<Source, Destination>();

                Mapper.AssertConfigurationIsValid(typeMap);


                
                var typeMap2 = Mapper.FindTypeMapFor<Source, Destination2>();
                Exception exception = null;
                try
                {
                    Mapper.AssertConfigurationIsValid(typeMap2);
                }
                catch (Exception e)
                {
                    exception = e;
                }
                exception.ShouldNotBeNull();
                exception.ShouldBeType<AutoMapperConfigurationException>();
            }
        }
    }

}