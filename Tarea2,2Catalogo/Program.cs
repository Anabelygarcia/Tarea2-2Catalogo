// Tarea 2.2 - Catalogo de ejercicios: conversion de iterativo a recursivo.
// Cada ejercicio tiene version iterativa y version recursiva.
// Se documenta el CASO BASE (condicion de salida) y el CASO RECURSIVO.

using System;
using System.Collections.Generic;
using System.Linq;

namespace CatalogoRecursion
{
    class Program
    {
        // ---------------------------------------------------------------
        // 1. FACTORIAL
        // ---------------------------------------------------------------
        // BEGIN FactorialIter
        static long FactorialIter(int n)
        {
            long resultado = 1;
            for (int i = 2; i <= n; i++)
            {
                resultado *= i;
            }
            return resultado;
        }
        // END FactorialIter

        // BEGIN FactorialRec
        static long FactorialRec(int n)
        {
            // CASO BASE: 0! = 1 y 1! = 1
            if (n <= 1)
            {
                return 1;
            }
            // CASO RECURSIVO: n! = n * (n-1)!
            return n * FactorialRec(n - 1);
        }
        // END FactorialRec

        // ---------------------------------------------------------------
        // 2. FIBONACCI
        // ---------------------------------------------------------------
        // BEGIN FibonacciIter
        static long FibonacciIter(int n)
        {
            long a = 0, b = 1;
            for (int i = 0; i < n; i++)
            {
                long temp = a + b;
                a = b;
                b = temp;
            }
            return a;
        }
        // END FibonacciIter

        // BEGIN FibonacciRec
        static long FibonacciRec(int n)
        {
            // CASOS BASE: F(0) = 0 y F(1) = 1
            if (n == 0) return 0;
            if (n == 1) return 1;
            // CASO RECURSIVO: F(n) = F(n-1) + F(n-2)
            return FibonacciRec(n - 1) + FibonacciRec(n - 2);
        }
        // END FibonacciRec

        // ---------------------------------------------------------------
        // 3. TORRES DE HANOI
        // ---------------------------------------------------------------
        // BEGIN HanoiIter
        // Version iterativa con pila explicita (simula la recursion).
        static List<(char, char)> HanoiIter(int n, char origen = 'A',
                                            char auxiliar = 'B', char destino = 'C')
        {
            var movimientos = new List<(char, char)>();
            var pila = new Stack<(int k, char o, char a, char d, bool listo)>();
            pila.Push((n, origen, auxiliar, destino, false));

            while (pila.Count > 0)
            {
                var (k, o, a, d, listo) = pila.Pop();
                if (k == 0) continue;

                if (listo)
                {
                    movimientos.Add((o, d));
                }
                else
                {
                    // Se apilan en orden inverso al que se ejecutaran
                    pila.Push((k - 1, a, o, d, false));
                    pila.Push((k, o, a, d, true));
                    pila.Push((k - 1, o, d, a, false));
                }
            }
            return movimientos;
        }
        // END HanoiIter

        // BEGIN HanoiRec
        static List<(char, char)> HanoiRec(int n, char origen = 'A',
                                           char auxiliar = 'B', char destino = 'C',
                                           List<(char, char)> movs = null)
        {
            if (movs == null) movs = new List<(char, char)>();

            // CASO BASE: sin discos no hay nada que mover
            if (n == 0)
            {
                return movs;
            }
            // CASO RECURSIVO:
            // 1) mover n-1 discos de origen a auxiliar
            HanoiRec(n - 1, origen, destino, auxiliar, movs);
            // 2) mover el disco mayor de origen a destino
            movs.Add((origen, destino));
            // 3) mover n-1 discos de auxiliar a destino
            HanoiRec(n - 1, auxiliar, origen, destino, movs);
            return movs;
        }
        // END HanoiRec

        // ---------------------------------------------------------------
        // 4. SUMA DE ARREGLOS
        // ---------------------------------------------------------------
        // BEGIN SumaIter
        static int SumaIter(int[] arr)
        {
            int total = 0;
            foreach (int x in arr)
            {
                total += x;
            }
            return total;
        }
        // END SumaIter

        // BEGIN SumaRec
        static int SumaRec(int[] arr, int i = 0)
        {
            // CASO BASE: indice fuera del arreglo -> suma vacia = 0
            if (i >= arr.Length)
            {
                return 0;
            }
            // CASO RECURSIVO: arr[i] + suma del resto
            return arr[i] + SumaRec(arr, i + 1);
        }
        // END SumaRec

        // ---------------------------------------------------------------
        // 5. BUSQUEDA LINEAL
        // ---------------------------------------------------------------
        // BEGIN BusquedaIter
        static int BusquedaIter(int[] arr, int objetivo)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == objetivo)
                {
                    return i;
                }
            }
            return -1;
        }
        // END BusquedaIter

        // BEGIN BusquedaRec
        static int BusquedaRec(int[] arr, int objetivo, int i = 0)
        {
            // CASO BASE 1: se recorrio todo el arreglo sin encontrarlo
            if (i >= arr.Length)
            {
                return -1;
            }
            // CASO BASE 2: se encontro el elemento
            if (arr[i] == objetivo)
            {
                return i;
            }
            // CASO RECURSIVO: seguir buscando en la siguiente posicion
            return BusquedaRec(arr, objetivo, i + 1);
        }
        // END BusquedaRec

        // ---------------------------------------------------------------
        // PRUEBAS
        // ---------------------------------------------------------------
        static void Verificar(bool condicion, string mensaje)
        {
            if (!condicion)
            {
                throw new Exception("PRUEBA FALLIDA: " + mensaje);
            }
        }

        static string Fmt(int[] arr)
        {
            return "[" + string.Join(", ", arr) + "]";
        }

        static void Main(string[] args)
        {
            foreach (int n in new[] { 0, 1, 5, 10 })
            {
                Verificar(FactorialIter(n) == FactorialRec(n), "factorial " + n);
                Console.WriteLine($"factorial({n}) = {FactorialRec(n)}");
            }

            foreach (int n in new[] { 0, 1, 2, 6, 10 })
            {
                Verificar(FibonacciIter(n) == FibonacciRec(n), "fibonacci " + n);
                Console.WriteLine($"fibonacci({n}) = {FibonacciRec(n)}");
            }

            foreach (int n in new[] { 1, 2, 3, 4 })
            {
                var r = HanoiRec(n);
                var it = HanoiIter(n);
                Verificar(r.SequenceEqual(it) && r.Count == (1 << n) - 1, "hanoi " + n);
                Console.WriteLine($"hanoi({n}) -> {r.Count} movimientos");
            }

            var arreglosSuma = new[]
            {
                new int[] { },
                new int[] { 7 },
                new int[] { 3, 5, 2, 8 }
            };
            foreach (var arr in arreglosSuma)
            {
                Verificar(SumaIter(arr) == SumaRec(arr), "suma " + Fmt(arr));
                Console.WriteLine($"suma({Fmt(arr)}) = {SumaRec(arr)}");
            }

            var pruebasBusqueda = new[]
            {
                (new int[] { 4, 9, 1, 7 }, 1),
                (new int[] { 4, 9, 1, 7 }, 5),
                (new int[] { }, 3)
            };
            foreach (var (arr, x) in pruebasBusqueda)
            {
                Verificar(BusquedaIter(arr, x) == BusquedaRec(arr, x), "busqueda " + x);
                Console.WriteLine($"busqueda({Fmt(arr)}, {x}) = {BusquedaRec(arr, x)}");
            }

            Console.WriteLine("TODAS LAS PRUEBAS PASARON");
        }
    }
}