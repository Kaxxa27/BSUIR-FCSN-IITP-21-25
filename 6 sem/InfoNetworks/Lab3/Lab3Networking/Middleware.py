class RSTMiddleware:

    def __init__(self):
        self.call_number = 0

    def change(self, package):
        self.call_number += 1
        if self.call_number == 5:
            package.rst = True
        return package


# class TcpResetMiddleware:
#
#     def __init__(self):
#         self.call_number = 0
#
#     def change(self, package):
#         self.call_number += 1
#         if self.call_number == 5:
#             package.rst = False
#         return package


class FakeIpAddressMiddleware:

    def __init__(self, ip_address, tcp_port):
        self.ip_address = ip_address
        self.tcp_port = tcp_port
        self.call_number = 0

    def change(self, package):
        self.call_number += 1
        if self.call_number == 5:
            package.ip.destination_ip = self.ip_address
            package.destination_port = self.tcp_port
        return package