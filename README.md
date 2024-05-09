# CDull
CDull is an extremely basic sublanguage and interpreter written in C#. It supports basic mathematic operators, user variables, pre-existing functions and pre-existing constant values. The only data type is floating point numbers. To use, download the exe from the latest release and place into an empty folder. Create the file ``MyProgram.cdull`` in the folder, write some code in it and run the exe.
It will run (and tell you of any errors) and print the result of the final line to the console if successful.

---

# How to write CDull
An example CDull program looks like this:
```
let x = 5
let y = 1.5 * 2
let z = pow((x) (y)) + 1.2
sin(piovertwo) + z
```
>[!Note]
> There are no ``;`` at the end of the lines, each one contains a single expression or assignment.

``let x = 5``
This line declares a variable named ``x`` and assigns it a value of ``5``.

``let y = 1.5 * 2``
This line declares a variable named ``y`` and assigns it the expression ``1.5 * 2``, which evaluates to ``3``.

>[!Warning]
> Variable names must be unique (you cannot reassign to existing variables). They can only contain letters, no numbers.<br/>
> You cannot use or assign to existing functions/constants, or assign to variables before they are declared.

``let z = pow((x) (y)) + 1.2``
This line declares a variable named ``z`` and assigns it to ``pow((x) (y)) + 1.2``, which evaluates as ``5^3 + 1.2 = 125 + 1.2 = 127.2``. Calling functions that take multiple parameters requires both of them to be contained within their own pair of ``()`` with no commas seperating them inside the functions own pair of ``()``.

``sin(piovertwo) + z``
This line evaluates the sin of pi/4 radians, and adds the variable ``z`` to it. As this is the final line, it does not need to assign this to a variable (although it could if it wished) and the result of this expression will be printed to the console.

---

# Documentation
<b>Existing Constants:</b>
- ``pioverfour`` - 0.7853982
- ``piovertwo``  - 1.5707964
- ``pi``         - 3.1415927
- ``twopi``, ``tau`` - 6.2831854
- ``euler`` - 2.7182817

<b>Existing Functions:</b>
- ``sin(x)`` - Returns the sine of the specified angle.
- ``cos(x)`` - Returns the cosine of the specified angle.
- ``tan(x)`` - Returns the tangent of the specified angle.
- ``loge(x)`` - Returns the natural (base e) logarithm of a specified number.
- ``log((x) (y))`` - Returns the logarithm of a specified number in a specified base.
- ``logtwo(x)`` - Returns the base 2 logarithm of a specified number.
- ``logeten(x)`` - Returns the base 10 logarithm of a specified number.
- ``abs(x)`` - Returns the absolute value of a single-precision floating-point number.
- ``floor(x)`` - Returns the largest integral value less than or equal to the specified single-precision floating-point number.
- ``ceiling(x)`` - Returns the smallest integral value that is greater than or equal to the specified single-precision floating-point number.
- ``pow((x) (y))`` - Returns a specified number raised to the specified power.
- ``atan(x)`` - Returns the angle whose tangent is the specified number.
- ``atantwo((x) (y))`` - Returns the angle whose tangent is the quotient of two specified numbers.


