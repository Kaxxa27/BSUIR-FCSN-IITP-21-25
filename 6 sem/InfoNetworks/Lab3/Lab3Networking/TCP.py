class TCP:

    def __init__(self, source_port, destination_port, ip):
        self.ip = ip
        self.source_port = source_port
        self.destination_port = destination_port
        self.sequence = 0
        self.acknowledgment = 0
        self.offset = 20  # there is no additional options
        self.ns = None
        self.cwr = None
        self.ece = None
        self.urg = None
        self.ack = False
        self.psh = None
        self.rst = False
        self.syn = False
        self.fin = False
        self.window_size = None
        self.checksum = 0
        self.urgent = None

    def __str__(self):
        return ('\n*****************************\n'
                '[Source IP]       : {}\n'
                '[Source Port]     : {}\n'
                '[Destination IP]  : {}\n'
                '[Destination Port]: {}\n'
                '[Sequence]        : {}\n'
                '[Acknowledgment]  : {}\n'
                '[Payload]         : "{}"\n'
                '******************************').format(self.ip.source_ip, self.source_port, self.ip.destination_ip,
                                self.destination_port, self.sequence, self.acknowledgment,
                                self.ip.payload)
