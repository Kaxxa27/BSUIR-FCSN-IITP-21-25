from sympy import *


def traverse_list(ls, a):
    if type(ls) == tuple:
        if ls[0] != 0 or ls[1] != 0:
            a.append(ls)
        return
    for i in range(len(ls)):
        traverse_list(ls[i], a)


def get_clean_intervals(ls, a):
    traverse_list(ls, a)
    return a


def get_sturm_row(equation):
    x, y = symbols('x y')
    sturm_row = list()
    y = equation
    sturm_row.append(y)

    y_2 = diff(y)
    sturm_row.append(y_2)

    r = y_2
    while degree(r) > 0:
        r = div(y, y_2)[1] * (-1)  # get remain
        sturm_row.append(r)
        y = y_2
        y_2 = r
    return sturm_row


def get_1root_small_interval(sturm_row, interval: tuple):
    result_intervals = []
    digit_changes = [0, 0]

    for inter_num, inter in enumerate(interval):
        func_values = list()
        prev_val = 0
        current_val = 0
        for i, func in enumerate(sturm_row):
            func_values.append(func.subs('x', inter))

            if i == 0:
                prev_val = func_values[i]
                continue
            current_val = func_values[i]

            if prev_val == 0:
                prev_val = current_val
                continue
            if current_val == 0:
                continue

            if prev_val * current_val < 0:
                digit_changes[inter_num] += 1
            prev_val = current_val

    if digit_changes[0] == digit_changes[1]:
        return 0, 0

    if abs(digit_changes[1] - digit_changes[0]) == 1:
        return interval[0], interval[1]
    else:
        middle = float(interval[1] - interval[0]) / 2
        result_intervals.append(get_1root_small_interval(sturm_row, (interval[0], interval[0] + middle)))
        result_intervals.append(get_1root_small_interval(sturm_row, (interval[0] + middle, interval[1])))

    return result_intervals


def get_1root_intervals(sturm_row, interval: tuple):
    root_intervals = list()
    digit_change_times = dict()
    for i in range(interval[0], interval[1] + 1):
        func_values = list()
        digit_changes_count = 0
        for num, func in enumerate(sturm_row):
            func_values.append(func.subs('x', i))
            if num == 0:
                continue
            if func_values[num] * func_values[num - 1] < 0:
                digit_changes_count += 1

        digit_change_times[i] = digit_changes_count

    for i in range(interval[0] + 1, interval[1] + 1):

        if digit_change_times[i - 1] - digit_change_times[i] > 1:
            result_intervals = get_1root_small_interval(sturm_row, (i - 1, i))
            clean_upped_intervals = []
            clean_upped_intervals = get_clean_intervals(result_intervals, clean_upped_intervals)

            for inter in clean_upped_intervals:
                root_intervals.append(list(inter))

        elif digit_change_times[i - 1] != digit_change_times[i]:
            root_intervals.append([i - 1, i])

    for i in range(1, len(root_intervals)):
        if root_intervals[i][0] == root_intervals[i - 1][1]:
            root_intervals[i][0] = root_intervals[i][0] + 0.00001

    return root_intervals
