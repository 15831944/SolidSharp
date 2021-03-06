﻿using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SolidSharp.Expressions.Extensions
{
	public static class SymbolicExpressionExtensions
	{
		public static bool IsOperation(this SymbolicExpression e)
			=> ((IExpression)e).IsOperation;

		public static bool IsUnaryOperation(this SymbolicExpression e)
			=> ((IExpression)e).IsUnaryOperation;

		public static bool IsBinaryOperation(this SymbolicExpression e)
			=> ((IExpression)e).IsBinaryOperation;

		public static bool IsVariadicOperation(this SymbolicExpression e)
			=> ((IExpression)e).IsVariadicOperation;

		public static bool NeedsParentheses(this SymbolicExpression e)
			=> ((IExpression)e).NeedsParentheses;

		public static bool IsNegation(this SymbolicExpression e)
			=> ((IExpression)e).IsNegation;

		public static bool IsAbsoluteValue(this SymbolicExpression e)
			=> ((IExpression)e).IsAbsoluteValue;

		public static bool IsAddition(this SymbolicExpression e)
			=> ((IExpression)e).IsAddition;

		public static bool IsSubtraction(this SymbolicExpression e)
			=> ((IExpression)e).IsSubtraction;

		public static bool IsMultiplication(this SymbolicExpression e)
			=> ((IExpression)e).IsMultiplication;

		public static bool IsDivision(this SymbolicExpression e)
			=> ((IExpression)e).IsDivision;

		public static bool IsPower(this SymbolicExpression e)
			=> ((IExpression)e).IsPower;

		public static bool IsRoot(this SymbolicExpression e)
			=> ((IExpression)e).IsRoot;

		public static bool IsLn(this SymbolicExpression e)
			=> ((IExpression)e).IsLn;

		public static bool IsNumber(this SymbolicExpression e)
			=> ((IExpression)e).IsNumber;

		public static bool IsPositiveNumber(this SymbolicExpression e)
			=> ((IExpression)e).IsPositiveNumber;

		public static bool IsNegativeNumber(this SymbolicExpression e)
			=> ((IExpression)e).IsNegativeNumber;

		public static bool IsOddNumber(this SymbolicExpression e)
			=> ((IExpression)e).IsOddNumber;

		public static bool IsEvenNumber(this SymbolicExpression e)
			=> ((IExpression)e).IsEvenNumber;

		public static bool IsSimpleFraction(this SymbolicExpression e)
			=> ((IExpression)e).IsSimpleFraction;

		public static bool IsZero(this SymbolicExpression e)
			=> ReferenceEquals(e, NumberExpression.Zero); // This will work because we're making sure that Zero is a singleton.

		public static bool IsOne(this SymbolicExpression e)
			=> ReferenceEquals(e, NumberExpression.One); // This will work because we're making sure that One is a singleton.

		public static bool IsMinusOne(this SymbolicExpression e)
			=> ReferenceEquals(e, NumberExpression.MinusOne); // This will work because we're making sure that MinusOne is a singleton.

		public static bool IsConstant(this SymbolicExpression e)
			=> ((IExpression)e).IsConstant;

		public static bool IsVariable(this SymbolicExpression e)
			=> ((IExpression)e).IsVariable;

		/// <summary>Gets the name of the variable or constant.</summary>
		/// <returns>The name of the symbol represented by the current expression.</returns>
		/// <exception cref="NotSupportedException">This expression is neither a variable nor a constant.</exception>
		public static string GetName(this SymbolicExpression e)
			=> ((IExpression)e).Name;

		/// <summary>Gets the precedence of the operation.</summary>
		/// <returns>A value indicating the priority of the operation, the lower the highest.</returns>
		/// <exception cref="NotSupportedException">This expression is not an operation.</exception>
		public static byte GetPrecedence(this SymbolicExpression e)
			=> ((IExpression)e).GetPrecedence();

		/// <summary>Gets the numeric value of the expression.</summary>
		/// <remarks>This only worlks for numeric expressions.</remarks>
		/// <returns>The value of the expression.</returns>
		/// <exception cref="InvalidCastException">Expression is not a <see cref="NumberExpression"/>.</exception>
		public static long GetValue(this SymbolicExpression e)
			=> ((NumberExpression)e).Value;

		/// <summary>Gets the operand of the expression.</summary>
		/// <remarks>
		/// This method will only succeed for expressions that represent unary operations.
		/// You should always verify the kind of the expression before trying to retrieve its operands.
		/// </remarks>
		/// <param name="e">The expression.</param>
		/// <returns>The operand of the expression.</returns>
		/// <exception cref="NotSupportedException">This kind of expression doesn't have exactly one operand.</exception>
		public static SymbolicExpression GetOperand(this SymbolicExpression e)
			=> ((IExpression)e).GetOperand();

		/// <summary>Gets the first operand of the expression.</summary>
		/// <remarks>
		/// This method will only succeed for expressions that represent any kind of operations.
		/// You should always verify the kind of the expression before trying to retrieve its operands.
		/// </remarks>
		/// <param name="e">The expression.</param>
		/// <returns>The first operand of the expression.</returns>
		/// <exception cref="NotSupportedException">This kind of expression doesn't have exactly one operand.</exception>
		public static SymbolicExpression GetFirstOperand(this SymbolicExpression e)
			=> ((IExpression)e).GetFirstOperand();

		/// <summary>Gets the second operand of the expression.</summary>
		/// <remarks>
		/// This method will only succeed for expressions that represent an operation with at least two operands.
		/// You should always verify the kind of the expression before trying to retrieve its operands.
		/// </remarks>
		/// <param name="e">The expression.</param>
		/// <returns>The second operand of the expression.</returns>
		/// <exception cref="NotSupportedException">This kind of expression doesn't have exactly one operand.</exception>
		public static SymbolicExpression GetSecondOperand(this SymbolicExpression e)
			=> ((IExpression)e).GetSecondOperand();

		/// <summary>Gets the operands of the expression.</summary>
		/// <remarks>
		/// This method will only succeed for expressions that represent n-ary operations.
		/// You should always verify the kind of the expression before trying to retrieve its operands.
		/// </remarks>
		/// <param name="e">The expression.</param>
		/// <returns>An array containing the operands of the expression.</returns>
		/// <exception cref="NotSupportedException">This kind of expression doesn't have any operand.</exception>
		public static ReadOnlySpan<SymbolicExpression> GetOperands(this SymbolicExpression e)
			=> ((IExpression)e).GetOperands();

		/// <summary>Gets the factors composing an expression.</summary>
		/// <param name="e">The expression whose factors should be returned.</param>
		/// <returns>A read-only span of the factors composing the expression.</returns>
		public static ReadOnlySpan<SymbolicExpression> GetFactors(ref SymbolicExpression e)
			=> e.IsMultiplication() ?
				e.GetOperands() :
				MemoryMarshal.CreateReadOnlySpan(ref e, 1);
	}
}
