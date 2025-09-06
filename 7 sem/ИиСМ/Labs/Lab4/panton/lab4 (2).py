import tkinter as tk
from tkinter import ttk
from math import factorial

class ShopSimulation:
    def __init__(self):
        # Default values (can be modified via GUI)
        self.arrival_rate = 45 / 60  # customers per minute
        self.selection_time = 5      # minutes
        self.payment_time = 1        # minutes
        self.control_time = 1        # minutes
        self.selection_points = 4    # servers for selection
        self.payment_points = 1      # servers for payment (cashiers)
        self.control_points = 3      # servers for control

    def calculate_utilization(self, service_time, servers):
        return (self.arrival_rate * service_time) / servers

    def erlang_c(self, service_time, servers):
        rho = self.calculate_utilization(service_time, servers)
        a = self.arrival_rate * service_time
        sum_terms = sum([(a ** n) / factorial(n) for n in range(servers)])
        term = ((a ** servers) / (factorial(servers) * (1 - rho)))
        P0 = 1 / (sum_terms + term)
        Pc = (term * P0)  # Probability of waiting
        return Pc

    def average_queue_length(self, service_time, servers):
        rho = self.calculate_utilization(service_time, servers)
        Pc = self.erlang_c(service_time, servers)
        if rho >= 1:
            return float('inf')
        return (Pc * rho) / (1 - rho)

    def average_waiting_time(self, service_time, servers):
        rho = self.calculate_utilization(service_time, servers)
        if rho >= 1:
            return float('inf')  # Infinity if the system is overloaded
        Lq = self.average_queue_length(service_time, servers)
        return Lq / self.arrival_rate

    def calculate_metrics(self):
        utilization_selection = self.calculate_utilization(self.selection_time, self.selection_points)
        utilization_payment = self.calculate_utilization(self.payment_time, self.payment_points)
        utilization_control = self.calculate_utilization(self.control_time, self.control_points)

        waiting_time_selection = self.average_waiting_time(self.selection_time, self.selection_points)
        waiting_time_payment = self.average_waiting_time(self.payment_time, self.payment_points)
        waiting_time_control = self.average_waiting_time(self.control_time, self.control_points)

        # Check if waiting time is very high or infinity (overloaded system)
        results = {
            "Utilization (Selection)": utilization_selection,
            "Utilization (Payment)": utilization_payment,
            "Utilization (Control)": utilization_control,
            "Average Waiting Time (Selection)": waiting_time_selection if waiting_time_selection != float('inf') else "Система перегружена",
            "Average Waiting Time (Payment)": waiting_time_payment if waiting_time_payment != float('inf') else "Система перегружена",
            "Average Waiting Time (Control)": waiting_time_control if waiting_time_control != float('inf') else "Система перегружена"
        }

        return results

class ShopSimulationRecommendations(ShopSimulation):
    def provide_recommendations(self):
        # Calculate metrics
        utilization_selection = self.calculate_utilization(self.selection_time, self.selection_points)
        utilization_payment = self.calculate_utilization(self.payment_time, self.payment_points)
        utilization_control = self.calculate_utilization(self.control_time, self.control_points)

        recommendations = []

        if utilization_selection > 0.75:
            recommendations.append("Звено: Выбор товаров - Рекомендуется увеличить количество точек выбора или ускорить процесс выбора товаров, чтобы снизить загрузку и уменьшить время ожидания.")

        if utilization_payment > 0.75:
            recommendations.append("Звено: Оплата - Рекомендуется добавить еще одну кассу или сократить время оплаты для уменьшения очередей.")

        if utilization_control > 0.75:
            recommendations.append("Звено: Контроль - Рассмотрите возможность увеличения числа контрольных точек или ускорения процесса контроля.")

        if not recommendations:
            recommendations.append("Система работает эффективно. Значительных задержек в обслуживании не обнаружено.")

        return recommendations

# Create the tkinter UI
class ShopApp:
    def __init__(self, root):
        self.simulation = ShopSimulationRecommendations()
        self.root = root
        self.root.title("Shop Queue Simulation")
        self.create_widgets()

    def create_widgets(self):
        # Labels and Entries for Input
        ttk.Label(self.root, text="Arrival Rate (customers/hour)").grid(row=0, column=0)
        self.arrival_entry = ttk.Entry(self.root)
        self.arrival_entry.grid(row=0, column=1)
        self.arrival_entry.insert(0, "45")

        ttk.Label(self.root, text="Selection Points").grid(row=1, column=0)
        self.selection_points_entry = ttk.Entry(self.root)
        self.selection_points_entry.grid(row=1, column=1)
        self.selection_points_entry.insert(0, "4")

        ttk.Label(self.root, text="Avg. Time for Selection (min)").grid(row=2, column=0)
        self.selection_time_entry = ttk.Entry(self.root)
        self.selection_time_entry.grid(row=2, column=1)
        self.selection_time_entry.insert(0, "5")

        ttk.Label(self.root, text="Avg. Time for Payment (min)").grid(row=3, column=0)
        self.payment_time_entry = ttk.Entry(self.root)
        self.payment_time_entry.grid(row=3, column=1)
        self.payment_time_entry.insert(0, "1")

        ttk.Label(self.root, text="Payment Points (Cashiers)").grid(row=4, column=0)
        self.payment_points_entry = ttk.Entry(self.root)
        self.payment_points_entry.grid(row=4, column=1)
        self.payment_points_entry.insert(0, "1")

        ttk.Label(self.root, text="Control Points").grid(row=5, column=0)
        self.control_points_entry = ttk.Entry(self.root)
        self.control_points_entry.grid(row=5, column=1)
        self.control_points_entry.insert(0, "3")

        ttk.Label(self.root, text="Avg. Time for Control (min)").grid(row=6, column=0)
        self.control_time_entry = ttk.Entry(self.root)
        self.control_time_entry.grid(row=6, column=1)
        self.control_time_entry.insert(0, "1")

        # Button
        self.calculate_button = ttk.Button(self.root, text="Calculate", command=self.calculate)
        self.calculate_button.grid(row=7, column=0, columnspan=2)

        # Result Output
        self.result_text = tk.Text(self.root, width=50, height=10)
        self.result_text.grid(row=8, column=0, columnspan=2)

        # Recommendations Output
        self.recommendations_text = tk.Text(self.root, width=50, height=10)
        self.recommendations_text.grid(row=9, column=0, columnspan=2)

    def calculate(self):
        try:
            # Update simulation parameters based on user inputs
            arrival_rate = float(self.arrival_entry.get()) / 60
            selection_points = int(self.selection_points_entry.get())
            selection_time = float(self.selection_time_entry.get())
            payment_time = float(self.payment_time_entry.get())
            payment_points = int(self.payment_points_entry.get())
            control_points = int(self.control_points_entry.get())
            control_time = float(self.control_time_entry.get())

            self.simulation.arrival_rate = arrival_rate
            self.simulation.selection_points = selection_points
            self.simulation.selection_time = selection_time
            self.simulation.payment_time = payment_time
            self.simulation.payment_points = payment_points
            self.simulation.control_points = control_points
            self.simulation.control_time = control_time

            # Calculate metrics
            results = self.simulation.calculate_metrics()
            self.result_text.delete(1.0, tk.END)
            for key, value in results.items():
                if isinstance(value, float):
                    formatted_value = f"{value:.6f}"  # Format to 6 decimal places
                else:
                    formatted_value = value
                self.result_text.insert(tk.END, f"{key}: {formatted_value}\n")

            # Provide recommendations
            recommendations = self.simulation.provide_recommendations()
            self.recommendations_text.delete(1.0, tk.END)
            for rec in recommendations:
                self.recommendations_text.insert(tk.END, f"{rec}\n")

        except Exception as e:
            self.result_text.insert(tk.END, f"Error: {e}")


if __name__ == "__main__":
    root = tk.Tk()
    app = ShopApp(root)
    root.mainloop()
