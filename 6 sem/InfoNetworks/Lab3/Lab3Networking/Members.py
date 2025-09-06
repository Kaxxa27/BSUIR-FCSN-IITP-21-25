from IP import IP
from TCP import TCP


class Member:

    def __init__(self, ip_address, tcp_port):
        self.ip_address = ip_address
        self.tcp_port = tcp_port
        self.caller = False

    def callAnyOther(self, connection):
        self.caller = True
        other = connection.members[1]
        package = self.__build_package(other, self.__generate_payload())
        connection.connect(package)

    def __build_package(self, receiver, payload):
        ip = IP(self.ip_address, receiver.ip_address, "")
        tcp = TCP(self.tcp_port, receiver.tcp_port, ip)
        tcp.sequence = 0
        tcp.syn = True
        return tcp

    def _build_answer(self, package, payload):
        ip = IP(package.ip.destination_ip, package.ip.source_ip, "")
        tcp = TCP(package.destination_port, package.source_port, ip)

        if package.syn and package.ack:
            tcp.sequence = package.acknowledgment
            tcp.acknowledgment = package.sequence + 1
            tcp.ack = True
            return tcp
        if package.syn:
            tcp.sequence = 0
            tcp.acknowledgment = package.sequence + 1
            tcp.syn = True
            tcp.ack = True
            return tcp
        if package.ack:
            tcp.sequence = package.acknowledgment
            tcp.acknowledgment = len(payload)
            tcp.ip.payload = "Dummy package"
            return tcp

        tcp.ip.payload = payload

        if self.caller:
            tcp.sequence = package.acknowledgment
            tcp.acknowledgment = package.sequence + len(payload)
        else:
            tcp.sequence = package.acknowledgment
            tcp.acknowledgment = package.sequence
        return tcp

    def __generate_payload(self):
        return "A payload for member with address {}:{}".format(self.ip_address, self.tcp_port)

    def receive(self, package):
        answer = self._build_answer(package, self.__generate_payload())
        return answer


class HackerMember(Member):
    def __init__(self, ip_address, tcp_port):
        self.ip_address = ip_address
        self.tcp_port = tcp_port
        self.caller = False

    def receive(self, package):
        answer = self._build_answer(package, "Hacker server payload")
        return answer
