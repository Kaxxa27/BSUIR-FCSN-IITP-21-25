import _thread
from queueing.generator import Generator
from queueing.queueing import Queueing
from queueing.ui import Ui

priorities_number = 1
priorities_probability = [1]

ui = Ui()
generator = Generator()

stop = False
processing = False


def run():
    if globals()['processing'] or not ui.check_params():
        return
    globals()['processing'] = True
    ui.prepare_new_run()
    average_claims_receiving_time = ui.get_average_claims_receiving_time()
    claims_number = ui.get_claims_number()
    average_claims_service_time = ui.get_average_claims_service_time()
    devices_number = ui.get_devices_number()
    storage_capacity = ui.get_storage_capacity()
    receiving_time = generator.generate_exp(average_claims_receiving_time, claims_number, True)
    service_time = generator.generate_exp(average_claims_service_time, claims_number, True)
    priorities = generator.generate_discrete(priorities_probability, claims_number)
    queueing = Queueing(devices_number, storage_capacity, priorities_number)
    sec = 0
    priority = priorities.pop(0)
    res = queueing.add_claim(sec, priority, service_time.pop(0))
    ui.change_scheme_after_add(res)
    last_claim_receiving = sec
    while True:
        sec += 1
        ui.set_time(sec)
        res = queueing.del_served_claims(sec)
        ui.change_scheme_after_del(res)
        average_queue_time = queueing.get_average_queue_time(sec)
        average_stay_time = average_queue_time + average_claims_service_time
        ui.set_usage_rate(round(1 - queueing.get_usage_rate(sec), 2))
        ui.set_average_queue_time(round(average_queue_time / 60, 2))
        ui.set_average_stay_time(round(average_stay_time / 60, 2))
        ui.set_queue_claims_number(queueing.get_queue_claims_number())
        ui.set_stay_claims_number(queueing.get_stay_claims_number())
        ui.set_absolute_bandwidth(round(len(queueing.served_claims) / sec * 60, 2))
        ui.set_relative_bandwidth(round(len(queueing.served_claims) / queueing.get_received_claims_number(), 2))
        if len(queueing.served_claims) >= claims_number or globals()['stop']:
            globals()['stop'] = False
            break
        if len(receiving_time) == 0:
            receiving_time = generator.generate_exp(average_claims_receiving_time, claims_number, True)
        if len(service_time) == 0:
            service_time = generator.generate_exp(average_claims_service_time, claims_number, True)
        if len(priorities) == 0:
            priorities = generator.generate_discrete(priorities_probability, claims_number)
        if sec - last_claim_receiving == receiving_time[0]:
            del receiving_time[0]
            priority = priorities.pop(0)
            res = queueing.add_claim(sec, priority, service_time.pop(0))
            ui.change_scheme_after_add(res)
            last_claim_receiving = sec
    globals()['processing'] = False


def stop_thread(): globals()['stop'] = True


ui.run_btn.configure(command=lambda: _thread.start_new_thread(run, ()))
ui.stop_btn.configure(command=stop_thread)

ui.start()
