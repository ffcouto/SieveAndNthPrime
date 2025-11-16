# 🔢 6k±1 Algorithm — Computing the n-th Prime Number  
### A new arithmetic–progression–based method for efficient prime generation

---

## 📌 About the Project

This repository contains the implementation of the **6k±1 Algorithm**, a new method designed to compute the *n*-th prime number efficiently by applying a sieve structured exclusively over the arithmetic progressions:

- **6k − 1**
- **6k + 1**

Since **all prime numbers greater than 3 must belong to one of these sequences**, the algorithm drastically reduces the set of candidate integers, resulting in faster sieving and lower memory usage.

This project was developed as part of a **Final Undergraduate Thesis**, with the goal of presenting a mathematical and computational method capable of outperforming classical sieves such as:

- Sieve of Eratosthenes  
- Sieve of Atkin  
- Primality tests such as Miller–Rabin and AKS  

---

## 🚀 Algorithm Objective

The purpose of this algorithm is to:

- Reduce the search domain to only numbers of the form **6k ± 1**
- Perform composite marking through optimized multiplicative patterns
- Retrieve the *n*-th prime number **directly**, without generating all prior primes
- Achieve faster execution time and significantly lower memory usage than classical sieves

---

## 🧠 How the Algorithm Works

The method relies on three core ideas:

### **1. Domain Reduction**
Only numbers of the form 6k−1 and 6k+1 are considered — eliminating **two-thirds of all integers** immediately.

### **2. Implicit Bidimensional Sieve**
The algorithm models two interleaved arithmetic progressions:

- Row 1 → 6k − 1  
- Row 2 → 6k + 1  

Each index in the boolean vector corresponds to a specific position in these sequences.

### **3. Composite Marking via Multiplicative Patterns**
The algorithm marks composites using three deterministic patterns:

- (6k−1 × 6m+1)  
- (6k−1 × 6m−1)  
- (6k+1 × 6m+1)

These patterns eliminate redundant marking operations common in classical sieves.

---

## 📊 Benchmarks

Performance tests were conducted comparing:

- **Sieve of Eratosthenes**
- **Sieve of Atkin**
- **6k±1 Algorithm (proposed method)**

### Summary of results:

| Algorithm       | Average Speed | Memory Usage | Results |
|-----------------|---------------|--------------|----------|
| **6k±1**        | Fastest in all ranges | Lowest memory usage | ✔ Best performer |
| Eratosthenes    | 2×–3× slower | ~2.5× more memory | — |
| Atkin           | 3×–5× slower | ~2.5× more memory | — |

➡ When increasing N by a factor of 10, execution times also grow by approximately **10×**, indicating near-linear scalability.  
➡ Memory consumption can be **up to 250% lower** than traditional sieves.

---

## 📁 Repository Structure

/src → Source code of the algorithm
/docs → Documentation, graphs, tables, explanations
/tests → Benchmarks and unit tests
README.md → This file

---

## 🛠️ How to Run

### Requirements
- .NET 6+ installed  
or  
- Any C# compatible environment

### Running the algorithm

```bash
git clone https://github.com/YOUR_USERNAME/YOUR_REPOSITORY.git
cd YOUR_REPOSITORY/src
dotnet run

## 📘 Example Usage (C#)

long n = 1000000;
long prime = SieveAndNthPrime(n);
Console.WriteLine($"The {n}-th prime is: {prime}");

## 📄 License

This project is licensed under the MIT License.
You are free to use, modify, and distribute the code.

## 🤝 Contributing

Contributions are welcome!
Suggestions, improvements, or additional implementations may be submitted via Pull Request.

## 📬 Contact

For questions or collaboration:

Author: Fabiano Couto
Email: ff.couto@hotmail.com

## ⭐ Support the Project

If this repository is useful, please leave a star on GitHub!
Your support helps improve the project and encourages future development.
