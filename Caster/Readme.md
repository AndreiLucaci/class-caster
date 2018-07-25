# ClassCaster - intent
Used to gracefully cast an interface to a concrete type

## Original issue
This is due to the SonarQube issue S3215

Needing to cast from an <code>interface</code> to a concrete type indicates that something is wrong with the abstractions in use, likely that
something is missing from the <code>interface</code>. Instead of casting to a discrete type, the missing functionality should be added to the
<code>interface</code>. Otherwise there is a risk of runtime exceptions.


```cs
public static class DowncastExampleProgram
{
  static void EntryPoint(IMyInterface interfaceRef)
  {
    MyClass1 class1 = (MyClass1)interfaceRef;  // Noncompliant
    int privateData = class1.Data;
    class1 = interfaceRef as MyClass1;  // Noncompliant
    if (class1 != null)
    {
      // ...
    }
  }
}
```

## Fix
A simple extension method on the generic object

```cs
public static class ClassCaster
{
    public static T Cast<T>(this object obj)
    {
        return (T) Convert.ChangeType(obj, typeof(T));
    }
}
```

## Throwables:
```cs
InvalidCastException
```
Is thrown if the cast is unsuccesfull.

## Examples - TestScenario\ClassCasterTests

### 1. Ok

```cs
[Test]
public void ClassCaster_DirectObject_CastsCorrectly()
{
    // arrange
    var concrete = new Concrete();
    IInterface concreteToInterface = concrete;

    // act
    var result = concreteToInterface.Cast<Concrete>();

    // assert
    Assert.AreSame(concrete, result);
}
```

### 2. Not Ok
```cs
[Test]
public void ClassCaster_WrongType_CastBreaks()
{
    // arrange
    var concrete = new Concrete();
    IInterface concreteToInterface = concrete;

    // act
    Assert.Throws<InvalidCastException>(() =>
    {
        var result = concreteToInterface.Cast<DifferentConcrete>();
    });
}
```