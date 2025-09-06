from .claim import Claim


class Queueing:
    def __init__(self, devices_number, storage_capacity, priorities_number):
        self.devices = [{'claim': False, 'busy_time': 0} for _ in range(devices_number)]
        self.storage_capacity = storage_capacity
        self.storage = [[] for _ in range(priorities_number)]
        self.refusals = []
        self.evictions = 0
        self.served_claims = []

    def __get_free_device_number__(self):
        for i, device in enumerate(self.devices):
            if not device['claim']:
                return i
        return -1

    def __get_queue_length__(self):
        length = 0
        for claims in self.storage:
            length += len(claims)
        return length

    def __get_eviction_candidate__(self):
        eviction_candidate = 0
        for i, device in enumerate(self.devices):
            if self.devices[eviction_candidate]['claim'] < device['claim']:
                eviction_candidate = i
        return eviction_candidate

    def __occupy_device__(self, cur_time, device_number, claim):
        claim.processing_start_time = cur_time
        self.devices[device_number]['claim'] = claim

    def __unclaim_device__(self, cur_time, device_number):
        claim = self.devices[device_number]['claim']
        self.devices[device_number]['busy_time'] += cur_time - claim.processing_start_time
        self.devices[device_number]['claim'] = False
        return claim

    def add_claim(self, receiving_time, priority, service_time):
        claim = Claim(receiving_time, priority, service_time)
        queue_length = self.__get_queue_length__()
        if queue_length < self.storage_capacity:
            free_device = self.__get_free_device_number__()
            if free_device != -1:
                self.__occupy_device__(receiving_time, free_device, claim)
                return {'device': free_device}
            else:
                eviction_candidate = self.__get_eviction_candidate__()
                if self.devices[eviction_candidate]['claim'].priority > priority:
                    self.evictions += 1
                    evicted_claim = self.__unclaim_device__(receiving_time, eviction_candidate)
                    if queue_length < self.storage_capacity:
                        evicted_claim.receiving_time += receiving_time - evicted_claim.processing_start_time
                        evicted_claim.processing_start_time = -1
                        self.storage[evicted_claim.priority - 1].append(evicted_claim)
                        res = {'device': eviction_candidate, 'evicted': 'queue'}
                    else:
                        self.refusals.append(evicted_claim)
                        res = {'device': eviction_candidate, 'evicted': 'refusal'}
                    self.__occupy_device__(receiving_time, eviction_candidate, claim)
                    return res
                else:
                    self.storage[priority - 1].append(claim)
                    return {'queue': '+1'}
        else:
            self.refusals.append(claim)
            return False

    def del_served_claims(self, cur_time):
        res = {'freed_devices': [], 'occupied_devices': []}
        for i, device in enumerate(self.devices):
            if device['claim'] and cur_time - device['claim'].processing_start_time == device['claim'].service_time:
                self.served_claims.append(self.__unclaim_device__(cur_time, i))
                res['freed_devices'].append(i)
                for claims in self.storage:
                    if len(claims) != 0:
                        claim = claims.pop(0)
                        self.__occupy_device__(cur_time, i, claim)
                        res['occupied_devices'].append(i)
                        break
        return res

    def get_usage_rate(self, time):
        return round(sum(map(lambda d: d['busy_time'], self.devices)) / time / len(self.devices), 2)

    def get_received_claims_number(self):
        return self.get_stay_claims_number() + len(self.refusals) + len(self.served_claims)

    def get_average_queue_time(self, cur_time):
        queue_claims_time = 0
        for claims in self.storage:
            for claim in claims:
                queue_claims_time += cur_time - claim.receiving_time
        for device in self.devices:
            if device['claim']:
                queue_claims_time += device['claim'].processing_start_time - device['claim'].receiving_time
        queue_claims_time += sum(map(lambda c: c.processing_start_time - c.receiving_time, self.served_claims))
        return round(queue_claims_time / (self.get_received_claims_number() - len(self.refusals)))

    def get_queue_claims_number(self):
        number = 0
        for claims in self.storage:
            number += len(claims)
        return number

    def get_stay_claims_number(self):
        return self.get_queue_claims_number() + len(list(filter(lambda d: d['claim'], self.devices)))
