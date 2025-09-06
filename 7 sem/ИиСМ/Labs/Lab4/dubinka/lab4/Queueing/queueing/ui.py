from tkinter import *
from .tools import *
from time import sleep


class Ui:
    spaces = []

    # scheme
    stream_lbl = 'Вход'
    queue_lbl = 'Очередь'
    devices_lbl = 'Канал'
    refusals_lbl = 'Отказы'
    served_lbl = 'Обработано'
    scheme = {stream_lbl: [], queue_lbl: [], devices_lbl: [], refusals_lbl: [], served_lbl: []}

    # params
    devices_number_lbl = 'Количество устройств:'
    storage_capacity_lbl = 'Очередь:'
    average_claims_receiving_time_lbl = 'Среднее время поступления заявок:'
    average_claims_service_time_lbl = 'Среднее время обработки заявок:'
    claims_number_lbl = 'Количество обработанных заявок:'
    delay_lbl = 'Задержка при визуализации:'
    params = {storage_capacity_lbl: [], average_claims_receiving_time_lbl: [],
              average_claims_service_time_lbl: [], claims_number_lbl: [], delay_lbl: []}

    # results
    time_lbl = 'Модельное время:'
    usage_rate_lbl = 'Вероятность простоя:'
    average_queue_time_lbl = 'Среднее время ожидания в очереди:'
    average_stay_time_lbl = 'Среднее время пребывания заявки в системе:'
    absolute_bandwidth_lbl = 'Абсолютная пропускная способность:'
    relative_bandwidth_lbl = 'Доля обслуживаемых заявок:'
    results = {time_lbl: [], usage_rate_lbl: [], average_queue_time_lbl: [], average_stay_time_lbl: [], absolute_bandwidth_lbl: [], relative_bandwidth_lbl: []}

    graph_data = {'usage_rate': [0], 'queue_claims_number': [0], 'stay_claims_number': [1],
                  'average_queue_claims_number': [0], 'average_stay_claims_number': [0]}

    def __init__(self):
        self.window = Tk()
        self.window.title("Система массового обслуживания")
        self.window.geometry('1050x350')

        # draw block1
        block1_cur_row = self.draw_values_block(self.params, 0, 0, False)
        block1_cur_row += 1

        self.error_lbl = Label(self.window, fg='red')
        self.error_lbl.grid(column=0, row=block1_cur_row)

        self.run_btn = Button(self.window, text="ПУСК!", bg='red', width=8)
        self.run_btn.grid(column=1, row=block1_cur_row)
        block1_cur_row += 2

        self.set_default_values_btn = Button(self.window, text="Параметры по умолчанию",
                                             command=self.set_default_values, bg='grey')
        self.set_default_values_btn.grid(column=0, row=block1_cur_row)

        self.stop_btn = Button(self.window, text="СТОП!", bg='red', width=8)
        self.stop_btn.grid(column=1, row=block1_cur_row)
        block1_cur_row += 1

        self.spaces.append(Label(self.window, height=2))
        self.spaces[-1].grid(column=0, row=block1_cur_row)
        block1_cur_row += 1

        # draw block2
        self.spaces.append(Label(self.window, width=1))
        self.spaces[-1].grid(column=2, row=0)

        self.draw_values_block(self.results, 0, 3)

        self.spaces.append(Label(self.window, width=1))
        self.spaces[-1].grid(column=5, row=0)

    def get_delay(self):
        return float(self.params[self.delay_lbl][1].get())

    def get_average_claims_receiving_time(self):
        return int(self.params[self.average_claims_receiving_time_lbl][1].get())

    def get_claims_number(self):
        return int(self.params[self.claims_number_lbl][1].get())

    def get_average_claims_service_time(self):
        return int(self.params[self.average_claims_service_time_lbl][1].get())

    def get_devices_number(self):
        return 1

    def get_storage_capacity(self):
        return int(self.params[self.storage_capacity_lbl][1].get())

    def set_default_values(self):
        for parameter in self.params.keys():
            self.params[parameter][1].delete(0, END)
        self.params[self.storage_capacity_lbl][1].insert(END, '3')
        self.params[self.average_claims_receiving_time_lbl][1].insert(END, '15')
        self.params[self.average_claims_service_time_lbl][1].insert(END, '12')
        self.params[self.claims_number_lbl][1].insert(END, '1000')
        self.params[self.delay_lbl][1].insert(END, '0.02')

    def set_scheme_device_color(self, number, color):
        self.scheme[self.devices_lbl][number]['bg'] = color

    def set_time(self, sec):
        self.results[self.time_lbl][1]['text'] = sec

    def set_usage_rate(self, rate):
        self.results[self.usage_rate_lbl][1]['text'] = rate
        self.graph_data['usage_rate'].append(rate)

    def set_queue_claims_number(self, number):
        self.graph_data['queue_claims_number'].append(number)

    def set_stay_claims_number(self, number):
        self.graph_data['stay_claims_number'].append(number)

    def set_average_queue_time(self, sec):
        self.results[self.average_queue_time_lbl][1]['text'] = sec

    def set_average_stay_time(self, sec):
        self.results[self.average_stay_time_lbl][1]['text'] = sec

    def set_average_queue_size(self, size):
        self.results[self.average_queue_size_lbl][1]['text'] = size
        self.graph_data['average_queue_claims_number'].append(size)

    def set_average_stay_size(self, size):
        self.results[self.average_stay_size_lbl][1]['text'] = size
        self.graph_data['average_stay_claims_number'].append(size)

    def set_absolute_bandwidth(self, bandwidth):
        self.results[self.absolute_bandwidth_lbl][1]['text'] = bandwidth

    def set_relative_bandwidth(self, bandwidth):
        self.results[self.relative_bandwidth_lbl][1]['text'] = bandwidth

    def change_scheme_queue(self, dif):
        old_queue = self.scheme[self.queue_lbl][1]['text'].split('/')
        self.scheme[self.queue_lbl][1]['text'] = str(int(old_queue[0]) + dif) + '/' + old_queue[1]

    def change_scheme_after_add(self, res):
        self.scheme[self.stream_lbl][0]['bg'] = 'red'
        sleep(self.get_delay())
        if res:
            if 'queue' in res:
                self.change_scheme_queue(1)
                # print('новая заявка в очереди')
            elif 'evicted' in res:
                self.set_scheme_device_color(res['device'] + 1, 'green')
                if res['evicted'] == 'queue':
                    self.change_scheme_queue(1)
                    # print('новая заявка вытеснила заявку с устройства', res['device'] + 1, 'в очередь')
                else:
                    self.del_scheme_claim(self.refusals_lbl)
                    # print('новая заявка вытеснила заявку с устройства', res['device'] + 1, 'в отказ')
                self.set_scheme_device_color(res['device'] + 1, 'red')
            elif 'device' in res:
                self.set_scheme_device_color(res['device'] + 1, 'red')
                # print('новая заявка заняла свободное устройство', res['device'] + 1)
        else:
            self.del_scheme_claim(self.refusals_lbl)
            # print('новая заявка в отказе')
        sleep(self.get_delay())
        self.scheme[self.stream_lbl][0]['bg'] = 'white'

    def change_scheme_after_del(self, res):
        for device in res['freed_devices']:
            self.set_scheme_device_color(device + 1, 'green')
            self.del_scheme_claim(self.served_lbl)
        if res['freed_devices']:
            # print('заявки освободили устройства:', res['freed_devices'])
            sleep(self.get_delay())
        for device in res['occupied_devices']:
            self.change_scheme_queue(-1)
            self.set_scheme_device_color(device + 1, 'red')
        if res['occupied_devices']:
            # print('заявки из очереди заняли устройства:', res['occupied_devices'])
            sleep(self.get_delay())

    def clear_scheme(self):
        for elem in self.scheme.keys():
            for label in self.scheme[elem]:
                label.grid_remove()
            self.scheme[elem] = []

    def clear_results(self):
        for res in self.results.keys():
            self.results[res][1]['text'] = ''

    def del_scheme_claim(self, to):
        self.scheme[to][1]['text'] = int(self.scheme[to][1]['text']) + 1

    def draw_values_block(self, dictionary, cur_row, cur_col, readonly=True):
        for key in dictionary.keys():
            dictionary[key].append(Label(self.window, text=key))
            dictionary[key][0].grid(column=cur_col, row=cur_row)
            if readonly:
                dictionary[key].append(Label(self.window, relief=RIDGE, width=8))
            else:
                dictionary[key].append(Entry(self.window, width=8))
                dictionary[key].append(0)
            dictionary[key][1].grid(column=cur_col + 1, row=cur_row)
            cur_row += 1
        return cur_row

    def draw_scheme(self):
        self.clear_scheme()
        cur_col = 6
        self.scheme[self.stream_lbl].append(
            Label(self.window, text=self.stream_lbl, relief=RIDGE, width=10, height=self.get_devices_number() + 2))
        self.scheme[self.stream_lbl][0].grid(column=cur_col, row=0, rowspan=self.get_devices_number() + 1)

        self.scheme[self.queue_lbl].append(Label(self.window, text=self.queue_lbl))
        self.scheme[self.queue_lbl][0].grid(column=cur_col + 1, row=0)
        self.scheme[self.queue_lbl].append(
            Label(self.window, text='0/' + self.params[self.storage_capacity_lbl][1].get(), relief=RIDGE, width=10,
                  height=self.get_devices_number()))
        self.scheme[self.queue_lbl][1].grid(column=cur_col + 1, row=1, rowspan=self.get_devices_number())

        self.scheme[self.devices_lbl].append(Label(self.window, text=self.devices_lbl))
        self.scheme[self.devices_lbl][0].grid(column=cur_col + 2, row=0)
        for i in range(self.get_devices_number()):
            self.scheme[self.devices_lbl].append(
                Label(self.window, relief=RIDGE, width=10, bg='green', height=1))
            self.scheme[self.devices_lbl][i + 1].grid(column=cur_col + 2, row=i + 1)

        self.scheme[self.refusals_lbl].append(Label(self.window, text=self.refusals_lbl))
        self.scheme[self.refusals_lbl][0].grid(column=cur_col, row=self.get_devices_number() + 1)
        self.scheme[self.refusals_lbl].append(Label(self.window, text='0', relief=RIDGE, width=10, bg='red'))
        self.scheme[self.refusals_lbl][1].grid(column=cur_col, row=self.get_devices_number() + 2)

        self.scheme[self.served_lbl].append(Label(self.window, text=self.served_lbl))
        self.scheme[self.served_lbl][0].grid(column=cur_col + 1, row=self.get_devices_number() + 1, columnspan=2)
        self.scheme[self.served_lbl].append(Label(self.window, text='0', relief=RIDGE, width=20, bg='green'))
        self.scheme[self.served_lbl][1].grid(column=cur_col + 1, row=self.get_devices_number() + 2, columnspan=2)

    def draw_new_graph(self, width, height, x_max, y_max, x_eps, y_eps, x_points_number, y_points_number):
        if y_max == 0:
            y_max = 1
        new_window = Toplevel(self.window)
        new_window.geometry(str(width) + 'x' + str(height))
        c = Canvas(new_window, width=width, height=height, bg="white")
        c.pack()
        x_start = 40
        y_start = height - 20
        c.create_line(x_start, height, x_start, 0, width=2, arrow=LAST)
        c.create_line(0, y_start, width, y_start, width=2, arrow=LAST)
        c.create_text(x_start // 2, (height + y_start) // 2, text='0')
        cur_point = {'x': x_start // 2, 'y': y_start}
        step_y = round((y_start - 30) / y_points_number)
        y_scale = step_y / y_max * y_points_number
        for i in range(step_y, y_start, step_y):
            value = round(i / y_scale, y_eps)
            cur_point['y'] -= step_y
            c.create_text(cur_point['x'], cur_point['y'], text=str(value))
            c.create_line(cur_point['x'] + x_start // 2 - 3, cur_point['y'], cur_point['x'] + x_start // 2 + 3,
                          cur_point['y'], width=2)
            if value >= y_max:
                break
        step_x = round((width - x_start - 30) / x_points_number)
        x_scale = step_x / x_max * x_points_number
        cur_point = {'x': x_start, 'y': (height + y_start) // 2}
        for i in range(step_x, width - x_start, step_x):
            value = round(i / x_scale, x_eps)
            cur_point['x'] += step_x
            c.create_text(cur_point['x'], cur_point['y'], text=str(value))
            c.create_line(cur_point['x'], cur_point['y'] - (height - y_start) // 2 - 3, cur_point['x'],
                          cur_point['y'] - (height - y_start) // 2 + 3, width=2)
            if value >= x_max:
                break
        return c, x_start, y_start, x_scale, y_scale

    def draw_distribution_graph(self, param, reverse=False):
        if not self.check_param(param):
            return
        lmbd = 1 / int(self.params[param][1].get())
        c, x_start, y_start, x_scale, y_scale = self.draw_new_graph(550, 550, 5, 1, 0, 2, 5, 20)
        dx = 0.001
        for i in range(int(5 / dx)):
            x = i * dx
            y = lmbd * exp(-lmbd * x)
            if reverse:
                y = 1 - y
            x_point = x_start + x * x_scale
            y_point = y_start - y * y_scale
            c.create_oval(x_point, y_point, x_point + 1, y_point + 1, fill='green', outline='green')

    def draw_time_graph(self, data):
        time = len(data)
        if time <= 1:
            return
        c, x_start, y_start, x_scale, y_scale = self.draw_new_graph(800, 400, time, max(data), 0, 2, 15, 5)
        for t in range(time):
            c.create_line(x_start + t * x_scale, y_start - data[t] * y_scale, x_start + (t + 1) * x_scale,
                          y_start - data[t] * y_scale, width=4, fill='green')

    def check_params(self):
        for param in self.params.keys():
            if not self.check_param(param):
                return False
        return True

    def check_param(self, param):
        self.error_lbl['text'] = ''
        value = self.params[param][1].get()
        if param != self.delay_lbl:
            if not is_pos_int(value):
                self.error_lbl['text'] = '\'{}\' не натуральное число.'.format(value)
                return False
        elif not is_pos_float(value):
            self.error_lbl['text'] = '\'{}\' не положительное число.'.format(value)
            return False
        return True

    def start(self):
        self.window.mainloop()

    def prepare_new_run(self):
        self.graph_data = {'usage_rate': [0], 'queue_claims_number': [0], 'stay_claims_number': [1],
                           'average_queue_claims_number': [0], 'average_stay_claims_number': [0]}
        self.draw_scheme()
        self.clear_results()
        self.set_time(0)
