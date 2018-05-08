﻿using System.Collections.Immutable;

namespace SolidSharp.Expressions
{
	internal interface IExpression
    {
		/// <summary>Gets a value indicating if the expression represents a unary operation.</summary>
		bool IsUnaryOperation { get; }
		/// <summary>Gets a value indicating if the expression represents a binary operation.</summary>
		bool IsBinaryOperation { get; }
		/// <summary>Gets a value indicating if the expression represents a n-ary operation.</summary>
		bool IsVariadicOperation { get; }

		/// <summary>Gets a value indicating if the expression represents a negation.</summary>
		bool IsNegation { get; }

		/// <summary>Gets a value indicating if the expression represents an addition.</summary>
		bool IsAddition { get; }
		/// <summary>Gets a value indicating if the expression represents a subtraction.</summary>
		bool IsSubtraction { get; }
		/// <summary>Gets a value indicating if the expression represents a multiplication.</summary>
		bool IsMultiplication { get; }
		/// <summary>Gets a value indicating if the expression represents a division.</summary>
		bool IsDivision { get; }
		/// <summary>Gets a value indicating if the expression represents a power.</summary>
		bool IsPower { get; }

		/// <summary>Gets a value indicating if the expression represents a mathematical function.</summary>
		bool IsMathematicalFunction { get; }

		/// <summary>Gets a value indicating if the expression represents a number.</summary>
		bool IsNumber { get; }
		/// <summary>Gets a value indicating if the expression represents an integer number.</summary>
		bool IsInteger { get; }
		/// <summary>Gets a value indicating if the expression represents a positive integer number.</summary>
		bool IsPositiveInteger { get; }
		/// <summary>Gets a value indicating if the expression represents a negative integer number.</summary>
		bool IsNegativeInteger { get; }

		/// <summary>Gets a value indicating if the expression represents a variable.</summary>
		bool IsVariable { get; }
		/// <summary>Gets a value indicating if the expression represents a mathematic constant.</summary>
		bool IsConstant { get; }

		/// <summary>Gets the operand of the expression.</summary>
		/// <remarks>
		/// This method will only succeed for expressions that represent unary operations.
		/// You should always verify the kind of the expression before trying to retrieve its operands.
		/// </remarks>
		/// <param name="e">The expression.</param>
		/// <returns>The operand of the expression.</returns>
		/// <exception cref="NotSupportedException">This kind of expression doesn't have exactly one operand.</exception>
		SymbolicExpression GetOperand();

		/// <summary>Gets the operands of the expression.</summary>
		/// <remarks>
		/// This method will only succeed for expressions that represent n-ary operations.
		/// You should always verify the kind of the expression before trying to retrieve its operands.
		/// </remarks>
		/// <param name="e">The expression.</param>
		/// <returns>An array containing the operands of the expression.</returns>
		/// <exception cref="NotSupportedException">This kind of expression doesn't have any operand.</exception>
		ImmutableArray<SymbolicExpression> GetOperands();
	}
}
