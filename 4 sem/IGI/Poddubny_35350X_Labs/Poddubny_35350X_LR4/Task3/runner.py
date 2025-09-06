from Task3 import exp

class Task3:
    
    @classmethod
    def run(cls):
        while True:
            user_input = input("Enter x (|x| < 1): ")
            try:
                x = float(user_input)
                if abs(x) >= 1:
                    raise ValueError
                break
            except ValueError:
                print("Wrong input. |x| must be < 1.")

        while True:
            user_input = input("Enter eps: ")
            try:
                eps = float(user_input)
                break
            except ValueError:
                print("Wrong input.")

        print(exp.taylor_exp(x, eps))
        
        analyzer = exp.SequenceAnalyzer(x, eps)
        print("\nSequence Statistics:")
        print(analyzer.get_stats_table())

        analyzer.plot_series_comparison()