import tkinter as tk
from tkinter import messagebox
import numpy as np
import networkx as nx
import matplotlib.pyplot as plt
from matplotlib.backends.backend_tkagg import FigureCanvasTkAgg
from scipy.stats import expon
from collections import deque


class Request:
    _id_counter = 1

    def __init__(self, arrival_time, service_time):
        self.arrival_time = arrival_time
        self.service_time = service_time
        self.is_processed = False
        self.req_id = Request._id_counter
        Request._id_counter += 1


class Channel:
    def __init__(self, channel_id):
        self.channel_id = channel_id
        self.end_time = 0

    def is_free(self, current_time):
        return current_time >= self.end_time

    def process_request(self, request, current_time):
        if self.is_free(current_time):
            self.end_time = current_time + request.service_time
            request.is_processed = True
            return True
        return False


def simulate_poisson_arrival(X):
    return expon.rvs(scale=1 / X)


def simulate_service_time(mu):
    return expon.rvs(scale=1 / mu)


def calculate_probabilities_with_help(n, X, phi_func, num_requests=1000):
    requests = []
    current_time = 0
    rejections = 0
    request_queue = deque()
    log_lines = []
    for _ in range(num_requests):
        arrival_interval = simulate_poisson_arrival(X)
        current_time += arrival_interval
        service_time = simulate_service_time(phi_func(1))
        requests.append(Request(current_time, service_time))

    channels = [Channel(i) for i in range(n)]
    state_counts = np.zeros(n + 1)

    for request in requests:
        request_queue.append(request)
        processed = False
        for channel in channels:
            if channel.process_request(request_queue[0], request.arrival_time):
                log_lines.append(f"Request {request.req_id} processed (with help): Arrival Time = {request.arrival_time:.2f}, Service Time = {request.service_time:.2f}, Channel {channel.channel_id}")
                request_queue.popleft()
                processed = True
                break

        if not processed:
            rejections += 1
            log_lines.append(f"Request {request.req_id} rejected (with help): Arrival Time = {request.arrival_time:.2f}, Service Time = {request.service_time:.2f}")

        state = sum(1 for ch in channels if not ch.is_free(request.arrival_time))
        state_counts[state] += 1

    probabilities = state_counts / num_requests
    rejection_probability = rejections / num_requests

    with open("logs_with_help.txt", "w") as f:
        f.writelines("\n".join(log_lines))

    return probabilities, rejection_probability


def calculate_probabilities_without_help(n, X, phi_func, num_requests=1000):
    requests = []
    current_time = 0
    rejections = 0
    log_lines = []
    for _ in range(num_requests):
        arrival_interval = simulate_poisson_arrival(X)
        current_time += arrival_interval
        service_time = simulate_service_time(phi_func(1))
        requests.append(Request(current_time, service_time))

    channels = [Channel(i) for i in range(n)]
    state_counts = np.zeros(n + 1)

    for request in requests:
        processed = False
        for channel in channels:
            if channel.is_free(request.arrival_time):
                channel.process_request(request, request.arrival_time)
                log_lines.append(f"Request {request.req_id} processed (without help): Arrival Time = {request.arrival_time:.2f}, Service Time = {request.service_time:.2f}, Channel {channel.channel_id}")
                processed = True
                break

        if not processed:
            rejections += 1
            log_lines.append(f"Request {request.req_id} rejected (without help): Arrival Time = {request.arrival_time:.2f}, Service Time = {request.service_time:.2f}")

        state = sum(1 for ch in channels if not ch.is_free(request.arrival_time))
        state_counts[state] += 1

    probabilities = state_counts / num_requests
    rejection_probability = rejections / num_requests

    with open("logs_without_help.txt", "w") as f:
        f.writelines("\n".join(log_lines))

    return probabilities, rejection_probability


def plot_graph_with_help(n, X, phi_func, title, probabilities):
    G = nx.DiGraph()

    for i in range(n + 1):
        if i < n:
            occupied_after_arrival = i + 1
            free_after_arrival = n - occupied_after_arrival
            G.add_edge(
                i, occupied_after_arrival,
                label=f'λ={X:.2f}\nСвободно: {free_after_arrival}\nЗанято: {occupied_after_arrival}'
            )

        if i > 0:
            mu_value = phi_func(i)
            occupied_after_service = i - 1
            free_after_service = n - occupied_after_service
            G.add_edge(
                i, occupied_after_service,
                label=f'μ={mu_value:.2f}\nСвободно: {free_after_service}\nЗанято: {occupied_after_service}'
            )

    pos = nx.spring_layout(G, k=2, seed=42)
    labels = {i: f'Состояние {i}\nP={probabilities[i]:.2f}' for i in range(n + 1)}
    edge_labels = nx.get_edge_attributes(G, 'label')

    fig, ax = plt.subplots(figsize=(10, 8))
    nx.draw(G, pos, ax=ax, with_labels=True, labels=labels, node_size=2000, node_color='lightblue')
    nx.draw_networkx_edge_labels(G, pos, edge_labels=edge_labels, ax=ax, font_size=8, label_pos=0.3)

    ax.set_title(title)
    plt.tight_layout()

    return fig


def plot_graph_without_help(n, X, phi_func, title, probabilities):
    G = nx.DiGraph()

    for i in range(n + 1):
        if i < n:
            occupied_after_arrival = i + 1
            free_after_arrival = n - occupied_after_arrival
            G.add_edge(
                i, occupied_after_arrival,
                label=f'λ={X:.2f}\nСвободно: {free_after_arrival}\nЗанято: {occupied_after_arrival}'
            )

        if i > 0:
            mu_value = phi_func(i)
            occupied_after_service = i - 1
            free_after_service = n - occupied_after_service
            G.add_edge(
                i, occupied_after_service,
                label=f'μ={mu_value:.2f}\nСвободно: {free_after_service}\nЗанято: {occupied_after_service}'
            )

    pos = nx.spring_layout(G, k=2, seed=42)
    labels = {i: f'Состояние {i}\nP={probabilities[i]:.2f}' for i in range(n + 1)}
    edge_labels = nx.get_edge_attributes(G, 'label')

    fig, ax = plt.subplots(figsize=(10, 8))
    nx.draw(G, pos, ax=ax, with_labels=True, labels=labels, node_size=2000, node_color='lightblue')
    nx.draw_networkx_edge_labels(G, pos, edge_labels=edge_labels, ax=ax, font_size=8, label_pos=0.3)

    ax.set_title(title)
    plt.tight_layout()

    return fig


def calculate_efficiency(probabilities, n, X):
    P_reject = probabilities[n]
    L = sum(k * probabilities[k] for k in range(n + 1))
    rho = L / n
    avg_free_channels = n - L
    avg_requests = L / (1 - P_reject) if P_reject < 1 else 0
    throughput = X * (1 - P_reject)
    avg_time_in_system = avg_requests / throughput if throughput > 0 else 0
    return P_reject, L, rho, avg_free_channels, avg_requests, throughput, avg_time_in_system


def show_graph_in_new_window(fig):
    new_window = tk.Toplevel(root)
    new_window.title("Граф состояний СМО")
    canvas = FigureCanvasTkAgg(fig, master=new_window)
    canvas.draw()
    canvas.get_tk_widget().pack()


def on_calculate():
    try:
        n = int(entry_n.get())
        X = float(entry_X.get())
        phi_expr = entry_phi.get()
        phi_func = lambda k: eval(phi_expr)

        probabilities_with_help, P_reject_with_help = calculate_probabilities_with_help(n, X, phi_func)
        probabilities_without_help, P_reject_without_help = calculate_probabilities_without_help(n, X, lambda k: 1)

        fig_with_help = plot_graph_with_help(n, X, phi_func, "Граф СМО с взаимопомощью", probabilities_with_help)
        show_graph_in_new_window(fig_with_help)
        fig_without_help = plot_graph_without_help(n, X, lambda k: 1, "Граф СМО без взаимопомощи", probabilities_without_help)
        show_graph_in_new_window(fig_without_help)

        text_result.delete('1.0', tk.END)
        text_result.insert(tk.END, f'Вероятность отказа с взаимопомощью: {P_reject_with_help:.4f}\n')
        text_result.insert(tk.END, f'Вероятность отказа без взаимопомощи: {P_reject_without_help:.4f}\n')

        P_reject_with_help, L_with_help, rho_with_help, avg_free_channels_with_help, avg_requests_with_help, throughput_with_help, avg_time_in_system_with_help = calculate_efficiency(probabilities_with_help, n, X)
        text_result.insert(tk.END, f'СМО с взаимопомощью:\n')
        text_result.insert(tk.END, f'Вероятность отказа: {P_reject_with_help:.4f}\n')
        text_result.insert(tk.END, f'Среднее количество занятых каналов: {L_with_help:.2f}\n')
        text_result.insert(tk.END, f'Загрузка системы: {rho_with_help:.4f}\n')
        text_result.insert(tk.END, f'Среднее количество свободных каналов: {avg_free_channels_with_help:.2f}\n')
        text_result.insert(tk.END, f'Среднее количество заявок: {avg_requests_with_help:.2f}\n')
        text_result.insert(tk.END, f'Абсолютная пропускная способность: {throughput_with_help:.2f}\n')
        text_result.insert(tk.END, f'Среднее время в системе: {avg_time_in_system_with_help:.2f}\n')

        P_reject_without_help, L_without_help, rho_without_help, avg_free_channels_without_help, avg_requests_without_help, throughput_without_help, avg_time_in_system_without_help = calculate_efficiency(probabilities_without_help, n, X)
        text_result.insert(tk.END, f'СМО без взаимопомощи:\n')
        text_result.insert(tk.END, f'Вероятность отказа: {P_reject_without_help:.4f}\n')
        text_result.insert(tk.END, f'Среднее количество занятых каналов: {L_without_help:.2f}\n')
        text_result.insert(tk.END, f'Загрузка системы: {rho_without_help:.4f}\n')
        text_result.insert(tk.END, f'Среднее количество свободных каналов: {avg_free_channels_without_help:.2f}\n')
        text_result.insert(tk.END, f'Среднее количество заявок: {avg_requests_without_help:.2f}\n')
        text_result.insert(tk.END, f'Абсолютная пропускная способность: {throughput_without_help:.2f}\n')
        text_result.insert(tk.END, f'Среднее время в системе: {avg_time_in_system_without_help:.2f}\n')

    except Exception as e:
        messagebox.showerror("Ошибка", str(e))


root = tk.Tk()
root.title("Система массового обслуживания")

frame = tk.Frame(root)
frame.pack(pady=10)

label_n = tk.Label(frame, text="Число каналов (n):")
label_n.grid(row=0, column=0, padx=5)
entry_n = tk.Entry(frame)
entry_n.grid(row=0, column=1, padx=5)

label_X = tk.Label(frame, text="Интенсивность поступления заявок (X):")
label_X.grid(row=1, column=0, padx=5)
entry_X = tk.Entry(frame)
entry_X.grid(row=1, column=1, padx=5)

label_phi = tk.Label(frame, text="Функция mu(k):")
label_phi.grid(row=2, column=0, padx=5)
entry_phi = tk.Entry(frame)
entry_phi.grid(row=2, column=1, padx=5)

button_calculate = tk.Button(root, text="Вычислить", command=on_calculate)
button_calculate.pack(pady=10)

text_result = tk.Text(root, height=15, width=60)
text_result.pack()

root.mainloop()