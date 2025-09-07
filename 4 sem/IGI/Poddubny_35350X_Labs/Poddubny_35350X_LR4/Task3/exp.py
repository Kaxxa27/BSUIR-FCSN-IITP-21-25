import math
import os
from prettytable import PrettyTable
from collections import Counter
import matplotlib.pyplot as plt
import numpy as np

def proxy(func):
    def wrapper(*args, **kwargs):
        table = PrettyTable()
        table.field_names = ["x", "n", "F(x)", "Math F(x)", "eps"]
        table.align["F(x)"] = "l"
        table.align["Math F(x)"] = "l"
        table.float_format = ".8"
        result = func(*args, **kwargs)
        table.add_row([args[0], result[0], result[1], 1/(1-args[0]), args[1]])
        return table
    return wrapper


class SequenceAnalyzer:
    def __init__(self, x, eps):
        self.x = x
        self.eps = eps
        self.sequence = self.generate_sequence()
    
    def generate_sequence(self):
        sequence = []
        partial_sum = 0
        n = 0
        while n <= 500:
            term = self.x ** n
            new_sum = partial_sum + term
            sequence.append(new_sum)
            if n > 0 and abs(new_sum - partial_sum) <= self.eps:
                break
            partial_sum = new_sum
            n += 1
        return sequence
    
    def mean(self):
        return sum(self.sequence) / len(self.sequence)
    
    def median(self):
        sorted_seq = sorted(self.sequence)
        n = len(sorted_seq)
        mid = n // 2
        if n % 2 == 0:
            return (sorted_seq[mid - 1] + sorted_seq[mid]) / 2
        else:
            return sorted_seq[mid]
    
    def mode(self):
        if not self.sequence:
            return None
        freq = Counter(self.sequence)
        max_freq = max(freq.values())
        modes = [k for k, v in freq.items() if v == max_freq]
        return modes if max_freq > 1 else None
    
    def variance(self):
        m = self.mean()
        return sum((x - m) ** 2 for x in self.sequence) / len(self.sequence)
    
    def std_dev(self):
        return math.sqrt(self.variance())
    
    def get_stats_table(self):
        stats_table = PrettyTable()
        stats_table.field_names = ["Parameter", "Value"]
        stats_table.align["Parameter"] = "l"
        stats_table.align["Value"] = "l"
        stats_table.float_format = ".6"
        
        seq_str = "[" + ", ".join(f"{x:.4f}" for x in self.sequence[:5]) + ", ..." + \
                 (f", {self.sequence[-1]:.4f}]" if len(self.sequence) > 5 else "]")
        
        stats_table.add_row(["Sequence", seq_str])
        stats_table.add_row(["Length", len(self.sequence)])
        stats_table.add_row(["Mean", self.mean()])
        stats_table.add_row(["Median", self.median()])
        stats_table.add_row(["Mode", self.mode() or "No mode"])
        stats_table.add_row(["Variance", self.variance()])
        stats_table.add_row(["Standard Deviation", self.std_dev()])
        return stats_table
    
    def plot_series_comparison(self):
        plt.figure(figsize=(12, 7))
        
        plt.plot(
            range(len(self.sequence)), 
            self.sequence, 
            'bo-', 
            markersize=4,
            label=f'Partial sums (x={self.x}, eps={self.eps})',
            alpha=0.7
        )
        
        n_points = 100
        x_vals = np.linspace(0, len(self.sequence)-1, n_points)
        exact_value = 1 / (1 - self.x)
        y_vals = [exact_value] * n_points
        
        plt.plot(
            x_vals, 
            y_vals, 
            'r-', 
            linewidth=2,
            label=f'Exact function: 1/(1-{self.x}) = {exact_value:.8f}'
        )
        
        plt.title(f'Series comparison for x={self.x}, ε={self.eps}', pad=20)
        plt.xlabel('Iteration (n)', labelpad=10)
        plt.ylabel('Value', labelpad=10)
        plt.grid(True, linestyle='--', alpha=0.5)
        plt.legend(bbox_to_anchor=(1.05, 1), loc='upper left')
        
        os.makedirs("Task3", exist_ok=True)
        plt.savefig("Task3/result.png", bbox_inches='tight', dpi=300)
        plt.close()


@proxy
def taylor_exp(x, eps):
    i = 1
    sum_ = 1
    previous_value = 0
    while i <= 500 and abs(sum_ - previous_value) > eps:
        previous_value = sum_
        sum_ += pow(x, i)
        i += 1
    return [i, sum_]

