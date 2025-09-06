import time



def print_package(package):
    # try:
    #     time.sleep(0.2)
    # except KeyboardInterrupt:
    #     raise BaseException("Interrupted by user")

    print("[NEXT PCKG]  {}".format(package))


class Connection:

    def __init__(self, members, middlewares):
        self.members = members
        self.middlewares = middlewares
        self.closed = False
        self.connected = False

    def __find_receiver(self, package):
        for member in self.members:
            if (member.ip_address == package.ip.destination_ip
                    and member.tcp_port == package.destination_port):
                return member

    def connect(self, package):
        self.connected = True
        self.process(package)

    def process(self, package):
        if not self.connected or self.closed:
            return

        print_package(package)

        for middleware in self.middlewares:
            package = middleware.change(package)

        if package.rst:
            print('Tcp was reset by rst flag')
            self.close()
            return

        package.ip.ttl -= 1
        if package.ip.ttl <= 0:
            print('Package ttl is expired')
            self.close()
            return

        receiver = self.__find_receiver(package)
        if receiver is None:
            print('Unknown destination {}:{}'.format(package.ip.destination_ip, package.destination_port))
            self.close()
            return

        package = receiver.receive(package)
        if package is None:
            print('One of members stop sending requests')
            self.close()
        else:
            self.process(package)

    def close(self):
        self.closed = True
        print('Connection is closed')


class ConnectionHijack:

    def __init__(self):
        self.call_number = 0

    def change(self, package):
        self.call_number += 1
        if self.call_number >= 5:
            package.ip.payload = "Connection hijacked"
            t = package.sequence
            package.sequence = package.acknowledgment
            package.acknowledgment = t + len(package.ip.payload)
            package.ip.destination_ip = package.ip.source_ip
            package.destination_port = package.source_port
            print_package(package)
        return package
