class Claim:
    def __init__(self, receiving_time, priority, service_time):
        self.receiving_time = receiving_time
        self.priority = priority
        self.service_time = service_time
        self.processing_start_time = -1

    def __lt__(self, other):
        return self.priority < other.priority or \
               self.priority == other.priority and self.receiving_time < other.receiving_time

    def __str__(self):
        return '[Заявка: время поступления={0}, приоритет={1}, время начала обслуживания={2}, время обслуживания={3}]'.format(
            str(self.receiving_time), str(self.priority), str(self.processing_start_time), str(self.service_time))

    def __repr__(self):
        return '[Заявка: время поступления={0}, приоритет={1}, время начала обслуживания={2}, время обслуживания={3}]'.format(
            str(self.receiving_time), str(self.priority), str(self.processing_start_time), str(self.service_time))
