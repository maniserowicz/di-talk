using Procent.DependencyInjection.app;
using Xunit;

namespace Procent.DependencyInjection.Tests
{
    public class PoorMansContainerTests
    {
        public interface IInterface { }
        public class ImplementationWithoutDependencies : IInterface { }
        public class ImplementationWithDependencies : IInterface
        {
            public readonly Dependency Dependency;
            public ImplementationWithDependencies(Dependency dependency)
            {
                Dependency = dependency;
            }
        }
        public class ImplementationWithMultipleCtors : IInterface
        {
            public ImplementationWithMultipleCtors()
            {

            }
            public ImplementationWithMultipleCtors(Dependency dependency)
            {
            }
        }
        public class Dependency { }

        readonly PoorMansContainer _container;

        public PoorMansContainerTests()
        {
            _container = new PoorMansContainer();
        }

        [Fact]
        public void resolves_registered_type_with_generics()
        {
            _container.RegisterType<ImplementationWithoutDependencies>();

            var resolved = _container.Resolve<ImplementationWithoutDependencies>();

            Assert.NotNull(resolved);
        }

        [Fact]
        public void resolves_registered_type_without_generics()
        {
            _container.RegisterType<ImplementationWithoutDependencies>();

            var resolved = _container.Resolve(typeof(ImplementationWithoutDependencies));

            Assert.NotNull(resolved);
        }

        [Fact]
        public void resolves_registered_interface()
        {
            _container.Register<IInterface, ImplementationWithoutDependencies>();

            var resolved = _container.Resolve<IInterface>();

            Assert.NotNull(resolved);
            Assert.IsType<ImplementationWithoutDependencies>(resolved);
        }

        [Fact]
        public void resolves_registered_type_with_dependencies()
        {
            _container.RegisterType<ImplementationWithDependencies>();
            _container.RegisterType<Dependency>();

            var resolved = _container.Resolve<ImplementationWithDependencies>();

            Assert.NotNull(resolved);
            Assert.NotNull(resolved.Dependency);
        }

        [Fact]
        public void throws_when_trying_to_resolve_type_with_multiple_ctors()
        {
            _container.Register<IInterface, ImplementationWithMultipleCtors>();

            var exc = Assert.Throws<MultipleConstructorFoundException>(
                () => _container.Resolve<IInterface>()
            );
            Assert.Equal(typeof(IInterface), exc.Type);
        }

        [Fact]
        public void throws_when_resolving_type_with_unregistered_dependencies()
        {
            _container.RegisterType<ImplementationWithDependencies>();

            var exc = Assert.Throws<TypeNotRegisteredException>(
                () => _container.Resolve<ImplementationWithDependencies>()
            );
            Assert.Equal(typeof(Dependency), exc.Type);
        }

        [Fact]
        public void throws_when_resolving_not_registered_type()
        {
            var exc = Assert.Throws<TypeNotRegisteredException>(
                () => _container.Resolve<ImplementationWithoutDependencies>()
            );
            Assert.Equal(typeof(ImplementationWithoutDependencies), exc.Type);
        }

        [Fact]
        public void throws_when_resolving_not_registered_interface()
        {
            var exc = Assert.Throws<TypeNotRegisteredException>(
                () => _container.Resolve<IInterface>()
            );
            Assert.Equal(typeof(IInterface), exc.Type);
        }
    }
}