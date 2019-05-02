"""
eduEdit
=======

`eduEdit` is a toolbox for main app, it contains necessary methods that EduEdit page requires.
"""

from functools import partial

from kivy.cache import Cache
from kivy.uix.spinner import Spinner

from func import database_api
import json

__author__ = "Muhammed Yasin Yildirim"


def on_pre_enter(self):
    """
    This method creates spinner for multiple choice question and updates question field widgets through server.
    :param self: It is for handling class structure.
    :return:
    """

    print "in"
    self.correct_answer = Spinner(text="Correct Answer",
                                  values=("A", "B", "C", "D", "E"),
                                  color=(1, 1, 1, 1),
                                  font_name="data/font/CaviarDreams_Bold.ttf",
                                  font_size=self.height / 40,
                                  background_normal="data/img/widget_purple_75.png",
                                  background_down="data/img/widget_purple_75_select.png",
                                  size_hint=(.4, .05),
                                  pos_hint={"center_x": .75, "center_y": .075}
                                  )
    self.correct_answer.option_cls.font_name = "data/font/CaviarDreams_Bold.ttf"
    self.correct_answer.option_cls.background_down = "data/img/widget_purple_75_select.png"
    self.correct_answer.bind(text=partial(on_correct_answer_select,
                                          self
                                          )
                             )
    self.add_widget(self.correct_answer)

    widget = [self.correct_answer,
              self.ids["input_input"],
              self.ids["input_output"],
              self.ids["input_answer_a"],
              self.ids["input_answer_b"],
              self.ids["input_answer_c"],
              self.ids["input_answer_d"],
              self.ids["input_answer_e"],
              self.ids["input_short_answer"]
              ]
    for name in widget:
        name.opacity = 0
        name.size_hint_y = 0

    check_1 = self.ids["check_1"]
    check_1.background_radio_normal = "data/img/widget_black_75_round.png"
    check_1.background_radio_down = "data/img/widget_black_75_round_select.png"
    check_1.bind(active=partial(on_type_select,
                                self,
                                "programming"
                                )
                 )

    check_2 = self.ids["check_2"]
    check_2.background_radio_normal = "data/img/widget_black_75_round.png"
    check_2.background_radio_down = "data/img/widget_black_75_round_select.png"
    check_2.bind(active=partial(on_type_select,
                                self,
                                "short_answer"
                                )
                 )

    check_3 = self.ids["check_3"]
    check_3.background_radio_normal = "data/img/widget_black_75_round.png"
    check_3.background_radio_down = "data/img/widget_black_75_round_select.png"
    check_3.bind(active=partial(on_type_select,
                                self,
                                "multiple_choice"
                                )
                 )

    # data_exam_questions = database_api.getExam(Cache.get("info", "token"),
    #                                            Cache.get("lect", "code"),
    #                                            Cache.get("lect", "exam")
    #                                            )["Questions"]
    self.data_selected_question = Cache.get("lect","question")

    # self.data_selected_question = data_exam_questions[Cache.get("lect",
    #                                                             "question"
    #                                                             )]

    self.Extra = json.loads(self.data_selected_question["extra"])
    self.question_type = self.Extra["type"]
    print "Type",self.question_type



    self.ids["txt_lect_name"].text = "Hello"
    self.ids["txt_exam_name"].text =  "ExamName"
    self.ids["txt_question_no"].text = "Question {number}".format(number=self.data_selected_question["num"]
                                                                  )

    self.ids["input_subject"].text = self.Extra["subject"]
    self.ids["input_tags"].text = self.Extra["tags"][0]
    self.ids["input_grade"].text = str(self.Extra["value"])

    if self.question_type == "programming":
        check_1.active = True
        print "Programming"
        self.ids["input_question_body"].text = self.Extra["text"]
        # self.ids["input_question_body"].text = self.data_selected_question["text"].replace("*[SEAS-SLASH-N]*",
        #                                                                                    "\n"
        #
        print self.Extra["inputs"][0][0]
        print self.Extra["outputs"][0][0]

        self.ids["input_input"].text = self.Extra["inputs"][0][0]
        self.ids["input_output"].text = self.Extra["outputs"][0][0]


        # self.ids["input_input"].text = [",".join(key) for key in self.data_selected_question["Test_Cases"].keys()][0]
        # self.ids["input_output"].text = [",".join(value) for value in self.data_selected_question["Test_Cases"].values()][0]
        print "Programming out"

    elif self.question_type == "short_answer":
        check_2.active = True
        self.ids["input_question_body"].text = self.data_selected_question["text"].replace("*[SEAS-SLASH-N]*",
                                                                                           "\n"
                                                                                           )
        self.ids["input_short_answer"].text = self.data_selected_question["answer"].replace("*[SEAS-SLASH-N]*",
                                                                                            "\n"
                                                                                            )
    elif self.question_type == "multiple_choice":
        check_3.active = True
        question_text = self.data_selected_question["text"].split("*[SEAS-CHOICES]*")
        question_choices = question_text[1].split("*[SEAS-SLASH-N]*")
        self.ids["input_question_body"].text = question_text[0].replace("*[SEAS-SLASH-N]*",
                                                                        "\n"
                                                                        )
        self.ids["input_answer_a"].text = question_choices[0].replace("[font=data/font/AndaleMono.ttf][b]A)[/b][/font] ",
                                                                      ""
                                                                      )
        self.ids["input_answer_b"].text = question_choices[1].replace("[font=data/font/AndaleMono.ttf][b]B)[/b][/font] ",
                                                                      ""
                                                                      )
        self.ids["input_answer_c"].text = question_choices[2].replace("[font=data/font/AndaleMono.ttf][b]C)[/b][/font] ",
                                                                      ""
                                                                      )
        self.ids["input_answer_d"].text = question_choices[3].replace("[font=data/font/AndaleMono.ttf][b]D)[/b][/font] ",
                                                                      ""
                                                                      )
        self.ids["input_answer_e"].text = question_choices[4].replace("[font=data/font/AndaleMono.ttf][b]E)[/b][/font] ",
                                                                      ""
                                                                      )
        self.correct_answer.text = self.data_selected_question["answer"]
        self.multiple_choice_answer = self.correct_answer.text
    on_type_select(self, self.question_type, None, None)
    print "Finish"


def on_correct_answer_select(self, spinner, text):
    """
    This method assigns option selected from multiple choice spinner as correct answer.
    :param self: It is for handling class structure.
    :param spinner: It is spinner for multiple choice question.
    :param text: It is option selected from multiple choice spinner.
    :return:
    """

    self.multiple_choice_answer = text


def on_type_select(self, name, checkbox, value):
    """
    This method assigns type selected from question type checkbox as question type and updates question field widgets.
    :param self: It is for handling class structure.
    :param name: It is name of selected question type.
    :param checkbox: It is checkbox to select question type.
    :param value: It is value selected from question type checkbox.
    :return:
    """

    if name == "programming":
        self.question_type = name
        self.ids["input_tags"].hint_text = "Method"

        widget = [self.correct_answer,
                  self.ids["input_answer_a"],
                  self.ids["input_answer_b"],
                  self.ids["input_answer_c"],
                  self.ids["input_answer_d"],
                  self.ids["input_answer_e"],
                  self.ids["input_short_answer"]
                  ]
        for ids in widget:
            ids.opacity = 0
            ids.size_hint_y = 0

        active = [self.ids["input_input"],
                  self.ids["input_output"]
                  ]
        for ids in active:
            ids.opacity = 1
            ids.size_hint_y = 0.3
    elif name == "short_answer":
        self.question_type = name
        self.ids["input_tags"].hint_text = "Tags"

        widget = [self.correct_answer,
                  self.ids["input_input"],
                  self.ids["input_output"],
                  self.ids["input_answer_a"],
                  self.ids["input_answer_b"],
                  self.ids["input_answer_c"],
                  self.ids["input_answer_d"],
                  self.ids["input_answer_e"]
                  ]
        for ids in widget:
            ids.opacity = 0
            ids.size_hint_y = 0

        self.ids["input_short_answer"].opacity = 1
        self.ids["input_short_answer"].size_hint_y = 0.675
    elif name == "multiple_choice":
        self.question_type = name
        self.ids["input_tags"].hint_text = "Tags"

        widget = [self.ids["input_input"],
                  self.ids["input_output"],
                  self.ids["input_short_answer"]
                  ]
        for ids in widget:
            ids.opacity = 0
            ids.size_hint_y = 0

        active = [self.ids["input_answer_a"],
                  self.ids["input_answer_b"],
                  self.ids["input_answer_c"],
                  self.ids["input_answer_d"],
                  self.ids["input_answer_e"]
                  ]
        for ids in active:
            ids.opacity = 1
            ids.size_hint_y = 0.1

        self.correct_answer.opacity = 1
        self.correct_answer.size_hint_y = 0.05


def on_submit(self):
    """
    This method creates new question with given information through server.
    :param self: It is for handling class structure.
    :return: It is boolean for handling unfilled fields.
    """

    ico_status = self.ids["ico_status"]
    ico_status.opacity = 0

    if not self.ids["input_grade"].text.strip():
        ico_status.opacity = 1
        ico_status.pos_hint = {"x": .0125, "center_y": .425}

        return False
    elif not self.ids["input_question_body"].text.strip():
        ico_status.opacity = 1
        ico_status.pos_hint = {"x": .0125, "center_y": .3}

        return False
    else:
        if self.question_type == "programming":
            print "Before"
            yson = {"type": self.question_type,
                    "subject": self.ids["input_subject"].text,
                    "text": self.ids["input_question_body"].text.replace("\n",
                                                                         "*[SEAS-SLASH-N]*"
                                                                         ),
                    "answer": None,
                    "inputs": [self.ids["input_input"].text.split("*[SEAS-DELIMITER]*")],
                    "outputs": [self.ids["input_output"].text.split("*[SEAS-DELIMITER]*")],
                    "value": int(self.ids["input_grade"].text),
                    "tags": self.ids["input_tags"].text.split("*[SEAS-DELIMITER]*")
                    }

            Cache.get("lect","question")["extra"] = json.dumps(yson)


            database_api.edit_question(self.data_selected_question["id"],yson)

            # database_api.edit_question(Cache.get("info", "token"),
            #                            Cache.get("lect", "code"),
            #                            Cache.get("lect", "code"),
            #                            self.data_selected_question["id"],
            #                            yson
            #                            )

            return True
        elif self.question_type == "short_answer":
            yson = {"type": self.question_type,
                    "subject": self.ids["input_subject"].text,
                    "text": self.ids["input_question_body"].text.replace("\n",
                                                                         "*[SEAS-SLASH-N]*"
                                                                         ),
                    "answer": self.ids["input_short_answer"].text.replace("\n",
                                                                          "*[SEAS-SLASH-N]*"
                                                                          ),
                    "inputs": None,
                    "outputs": None,
                    "value": int(self.ids["input_grade"].text),
                    "tags": self.ids["input_tags"].text.split("*[SEAS-DELIMITER]*")
                    }

            database_api.edit_question(Cache.get("info", "token"),
                                       Cache.get("lect", "code"),
                                       Cache.get("lect", "exam"),
                                       self.data_selected_question["ID"],
                                       yson
                                       )

            return True
        elif self.question_type == "multiple_choice":
            if not self.ids["input_answer_a"].text.strip():
                ico_status.opacity = 1
                ico_status.pos_hint = {"x": .9375, "center_y": .675}

                return False
            elif not self.ids["input_answer_b"].text.strip():
                ico_status.opacity = 1
                ico_status.pos_hint = {"x": .9375, "center_y": .55}

                return False
            elif not self.ids["input_answer_c"].text.strip():
                ico_status.opacity = 1
                ico_status.pos_hint = {"x": .9375, "center_y": .425}

                return False
            elif not self.ids["input_answer_d"].text.strip():
                ico_status.opacity = 1
                ico_status.pos_hint = {"x": .9375, "center_y": .3}

                return False
            elif not self.ids["input_answer_e"].text.strip():
                ico_status.opacity = 1
                ico_status.pos_hint = {"x": .9375, "center_y": .175}

                return False
            else:
                try:
                    yson = {"type": self.question_type,
                            "subject": self.ids["input_subject"].text,
                            "text": "{q}*[SEAS-CHOICES]*" \
                                    "[font=data/font/AndaleMono.ttf][b]A)[/b][/font] {a}\n" \
                                    "[font=data/font/AndaleMono.ttf][b]B)[/b][/font] {b}\n" \
                                    "[font=data/font/AndaleMono.ttf][b]C)[/b][/font] {c}\n" \
                                    "[font=data/font/AndaleMono.ttf][b]D)[/b][/font] {d}\n" \
                                    "[font=data/font/AndaleMono.ttf][b]E)[/b][/font] {e}".format(q=self.ids["input_question_body"].text,
                                                                                                 a=self.ids["input_answer_a"].text,
                                                                                                 b=self.ids["input_answer_b"].text,
                                                                                                 c=self.ids["input_answer_c"].text,
                                                                                                 d=self.ids["input_answer_d"].text,
                                                                                                 e=self.ids["input_answer_e"].text
                                                                                                 ).replace("\n",
                                                                                                           "*[SEAS-SLASH-N]*"
                                                                                                           ),
                            "answer": self.multiple_choice_answer,
                            "inputs": None,
                            "outputs": None,
                            "value": int(self.ids["input_grade"].text),
                            "tags": self.ids["input_tags"].text.split("*[SEAS-DELIMITER]*")
                            }

                    database_api.edit_question(Cache.get("info", "token"),
                                               Cache.get("lect", "code"),
                                               Cache.get("lect", "exam"),
                                               self.data_selected_question["ID"],
                                               yson
                                               )

                    return True
                except:
                    ico_status.opacity = 1
                    ico_status.pos_hint = {"x": .9375, "center_y": .075}

                    return False
        else:
            ico_status.opacity = 1
            ico_status.pos_hint = {"x": .9375, "center_y": .8}

            return False
