import numpy as np

class Task5:
    @classmethod
    def run(cls, n=5, m=5):
        print("creating array:")
        A = np.random.randint(0, 100, size=(n, m))
        print("matrix A:\n", A)
        
        print("\nindexing and slicing:")
        print("first element of matrix:", A[0, 0])
        print("last row of matrix:", A[-1, :])
        print("first column of matrix:", A[:, 0])
        
        print("\narray operations:")
        print("matrix multiplied by 2:\n", A * 2)
        print("sum of all matrix elements:", np.sum(A))
        
        print("\nsorting last row in ascending order:")
        last_row = A[-1, :].copy()
        last_row_sorted = np.sort(last_row)
        print("original last row:", last_row)
        print("sorted last row:", last_row_sorted)
        
        median_std = np.median(last_row_sorted)
        print("\nmedian (standard function):", median_std)
        
        median_formula = cls._calculate_median_formula(last_row_sorted)
        print("median (by formula):", median_formula)
        
        print("\nadditional statistical operations:")
        print("mean value:", np.mean(last_row_sorted))
        print("variance:", np.var(last_row_sorted))
        print("standard deviation:", np.std(last_row_sorted))
        
        if m >= 2:
            print("\ncorrelation coefficient between first two columns:")
            corr = np.corrcoef(A[:, 0], A[:, 1])
            print("correlation matrix:\n", corr)
    
    @staticmethod
    def _calculate_median_formula(arr):
        n = len(arr)
        if n % 2 == 1:
            return arr[n // 2]
        else:
            return (arr[n // 2 - 1] + arr[n // 2]) / 2
